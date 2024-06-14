'
' API Gateway: Cliente de API en Visual Basic.
' Copyright (C) API Gateway <https://www.apigateway.cl>
'
' Este programa es software libre: usted puede redistribuirlo y/o modificarlo
' bajo los términos de la GNU Lesser General Public License (LGPL) publicada
' por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
' o (a su elección) cualquier versión posterior de la misma.
'
' Este programa se distribuye con la esperanza de que sea útil, pero SIN
' GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
' PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
' Public License (LGPL) para obtener una información más detallada.
'
' Debería haber recibido una copia de la GNU Lesser General Public License
' (LGPL) junto a este programa. En caso contrario, consulte
' <http://www.gnu.org/licenses/lgpl.html>.
'

Imports System.Net.Http
Imports System.Net.Http.Headers
Imports System.Text
Imports Newtonsoft.Json

Public Class ApiClient
    ' Constantes
    Private Const __DEFAULT_URL As String = "https://apigateway.cl"
    Private Const __DEFAULT_VERSION As String = "v1"
    ' Variables para la interacción con la API
    Private token As String
    Private url As String
    Private headers As Dictionary(Of String, String)
    Private version As String
    Private raiseForStatus As Boolean
    Private client As HttpClient

    ''' <summary>
    ''' Cliente para interactuar con la API de API Gateway.
    ''' </summary>
    ''' <param name="token">String Token de autenticación del usuario. Si no se proporciona, se intentará obtener de una variable de entorno.</param>
    ''' <param name="url">String URL base de la API. Si no se proporciona, se usará una URL por defecto.</param>
    ''' <param name="version">String Versión de la API. Si no se proporciona, se usará una versión por defecto.</param>
    ''' <param name="raiseForStatus">Boolean Si se debe lanzar una excepción automáticamente para respuestas de Error HTTP. Por defecto es True.</param>
    Public Sub New(Optional token As String = Nothing, Optional url As String = Nothing, Optional version As String = Nothing, Optional raiseForStatus As Boolean = True)
        Me.token = ValidateToken(token)
        Me.url = ValidateUrl(url)
        Me.headers = GenerateHeaders()
        Me.version = If(version, __DEFAULT_VERSION)
        Me.raiseForStatus = raiseForStatus
        Me.client = New HttpClient()
    End Sub

    ''' <summary>
    ''' Valida y retorna el token de autenticación.
    ''' </summary>
    ''' <param name="token">String Token de autenticación a validar.</param>
    ''' <returns>String Token validado.</returns>
    ''' <exception cref="ApiException">Si el token no es válido o está ausente.</exception>
    Private Function ValidateToken(token As String) As String
        token = If(token, Environment.GetEnvironmentVariable("APIGATEWAY_API_TOKEN"))
        If String.IsNullOrEmpty(token) Then
            Throw New ApiException("Se debe configurar la variable de entorno: APIGATEWAY_API_TOKEN.")
        End If
        Return token.Trim()
    End Function

    ''' <summary>
    ''' Valida y retorna la URL base para la API.
    ''' </summary>
    ''' <param name="url">String URL a validar.</param>
    ''' <returns>String URL validada.</returns>
    ''' <exception cref="ApiException">Si la URL no es válida o está ausente.</exception>
    Private Function ValidateUrl(url As String) As String
        Dim tempUrl As String
        If (String.IsNullOrEmpty(Environment.GetEnvironmentVariable("APIGATEWAY_API_URL"))) Then
            tempUrl = __DEFAULT_URL
        Else
            tempUrl = Environment.GetEnvironmentVariable("APIGATEWAY_API_URL")
        End If
        Return If(String.IsNullOrEmpty(Me.url), tempUrl, Me.url).Trim()
    End Function

    ''' <summary>
    ''' Genera y retorna las cabeceras por defecto para las solicitudes.
    ''' </summary>
    ''' <returns>Dictionary(Of String, String) Cabeceras por defecto.</returns>
    Private Function GenerateHeaders() As Dictionary(Of String, String)
        Dim headers As New Dictionary(Of String, String) From {
            {"User-Agent", "API Gateway Cliente de API en Visual Basic."},
            {"ContentType", "application/json"},
            {"Accept", "application/json"},
            {"Authorization", $"Bearer {Me.token}"}
        }
        Return headers
    End Function

    ''' <summary>
    ''' Realiza una solicitud GET a la API.
    ''' </summary>
    ''' <param name="resource">String Recurso de la API a solicitar.</param>
    ''' <param name="headers">Dictionary(Of String, String) Cabeceras adicionales para la solicitud.</param>
    ''' <returns>HttpResponseMessage Respuesta de la solicitud.</returns>
    Public Function GetMethod(resource As String, Optional headers As Dictionary(Of String, String) = Nothing) As HttpResponseMessage
        Return Request(method:=HttpMethod.Get, resource:=resource, headers:=headers)
    End Function

    ''' <summary>
    ''' Realiza una solicitud DELETE a la API.
    ''' </summary>
    ''' <param name="resource">String Recurso de la API a solicitar.</param>
    ''' <param name="headers">Dictionary(Of String, String) Cabeceras adicionales para la solicitud.</param>
    ''' <returns>HttpResponseMessage Respuesta de la solicitud.</returns>
    Public Function DeleteMethod(resource As String, Optional headers As Dictionary(Of String, String) = Nothing) As HttpResponseMessage
        Return Request(method:=HttpMethod.Delete, resource:=resource, headers:=headers)
    End Function

    ''' <summary>
    ''' Realiza una solicitud POST a la API.
    ''' </summary>
    ''' <param name="resource">String Recurso de la API a solicitar.</param>
    ''' <param name="data">Dictionary(Of String, Object) Datos a enviar en la solicitud.</param>
    ''' <param name="headers">Dictionary(Of String, String) Cabeceras adicionales para la solicitud.</param>
    ''' <returns>HttpResponseMessage Respuesta de la solicitud.</returns>
    Public Function PostMethod(resource As String, Optional data As Dictionary(Of String, Object) = Nothing, Optional headers As Dictionary(Of String, String) = Nothing) As HttpResponseMessage
        Return Request(method:=HttpMethod.Post, resource:=resource, data:=data, headers:=headers)
    End Function

    ''' <summary>
    ''' Realiza una solicitud PUT a la API.
    ''' </summary>
    ''' <param name="resource">String Recurso de la API a solicitar.</param>
    ''' <param name="data">Dictionary(Of String, Object) Datos a enviar en la solicitud.</param>
    ''' <param name="headers">Dictionary(Of String, String) Cabeceras adicionales para la solicitud.</param>
    ''' <returns>HttpResponseMessage Respuesta de la solicitud.</returns>
    Public Function PutMethod(resource As String, Optional data As Dictionary(Of String, Object) = Nothing, Optional headers As Dictionary(Of String, String) = Nothing) As HttpResponseMessage
        Return Request(method:=HttpMethod.Put, resource:=resource, data:=data, headers:=headers)
    End Function

    ''' <summary>
    ''' Método privado para realizar solicitudes HTTP.
    ''' </summary>
    ''' <param name="method">String Método HTTP a utilizar.</param>
    ''' <param name="resource">String Recurso de la API a solicitar.</param>
    ''' <param name="data">Dictionary(Of String, Object) Datos a enviar en la solicitud (opcional).</param>
    ''' <param name="headers">Dictionary(Of String, String) Cabeceras adicionales para la solicitud (opcional).</param>
    ''' <returns>HttpResponseMessage Respuesta de la solicitud.</returns>
    ''' <exception cref="ApiException">Si el método HTTP no es soportado o si hay un error de conexión.</exception>
    Private Function Request(method As HttpMethod, resource As String, Optional data As Dictionary(Of String, Object) = Nothing, Optional headers As Dictionary(Of String, String) = Nothing) As HttpResponseMessage
        Dim ApiPath As String = $"/api/{Me.version}{resource}"
        Dim fullUrl As String = New Uri(New Uri(Me.url & "/"), ApiPath.TrimStart("/"c)).ToString()
        Dim requestMessage As HttpRequestMessage = New HttpRequestMessage(method, fullUrl)


        If data IsNot Nothing Then
            Dim jsonData = JsonConvert.SerializeObject(data)
            requestMessage.Content = New StringContent(jsonData, Encoding.UTF8, "application/json")
        End If
        If headers IsNot Nothing Then
            For Each header In headers
                requestMessage.Headers.Add(header.Key, header.Value)
            Next
        End If
        For Each header In Me.headers
            requestMessage.Headers.Add(header.Key, header.Value)
        Next

        Try
            Dim response = Me.client.SendAsync(requestMessage).Result
            Dim resp As HttpResponseMessage = Me.CheckAndReturnResponse(response)

            Return resp
        Catch ex As HttpRequestException
            Throw New ApiException($"Error en la solicitud: {ex.Message}")
        Catch ex As TaskCanceledException
            Throw New ApiException($"Error de timeout: {ex.Message}")
        Catch ex As Exception
            Throw New ApiException($"Error: {ex.Message}")
        End Try
    End Function

    ''' <summary>
    ''' Verifica la respuesta de la solicitud HTTP y maneja los errores.
    ''' </summary>
    ''' <param name="response">HttpResponseMessage Objeto de respuesta de requests.</param>
    ''' <returns>HttpResponseMessage Respuesta validada.</returns>
    ''' <exception cref="ApiException">Si la respuesta contiene un error HTTP.</exception>
    Private Function CheckAndReturnResponse(response As HttpResponseMessage) As HttpResponseMessage
        If response.StatusCode <> 200 AndAlso Me.raiseForStatus Then
            Dim errorMessage As String = response.Content.ReadAsStringAsync().Result
            Throw New ApiException($"Error HTTP: {errorMessage}")
        End If

        Return response.EnsureSuccessStatusCode()
    End Function
End Class