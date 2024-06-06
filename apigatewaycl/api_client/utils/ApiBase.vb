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

Public MustInherit Class ApiBase
    ' Variables a utilizar en ApiBase
    Protected client As ApiClient
    Protected auth As Dictionary(Of String, Dictionary(Of String, String))

    ''' <summary>
    ''' Clase base para las clases que consumen la API (wrappers).
    ''' </summary>
    ''' <param name="api_token">String Token de autenticación para la API.</param>
    ''' <param name="api_url">String URL base para la API.</param>
    ''' <param name="api_version">String Versión de la API.</param>
    ''' <param name="api_raise_for_status">Boolean Si se debe lanzar una excepción automáticamente para respuestas de error HTTP. Por defecto es True.</param>
    ''' <param name="kwargs">Dictionary (Of String, String) Argumentos adicionales para la autenticación.</param>
    Public Sub New(Optional api_token As String = Nothing, Optional api_url As String = Nothing, Optional api_version As String = Nothing, Optional api_raise_for_status As Boolean = True, Optional kwargs As Dictionary(Of String, String) = Nothing)
        Me.client = New ApiClient(api_token, api_url, api_version, api_raise_for_status)
        Me.auth = New Dictionary(Of String, Dictionary(Of String, String))()
        If kwargs IsNot Nothing Then
            Me.SetupAuth(kwargs)
        End If
    End Sub

    ''' <summary>
    ''' Configura la autenticación específica para la aplicación.
    ''' </summary>
    ''' <param name="kwargs">Dictionary(Of String, String) Argumentos clave-valor para configurar la autenticación.</param>
    Private Sub SetupAuth(kwargs As Dictionary(Of String, String))
        Dim usuario_rut As String = If(kwargs.ContainsKey("usuario_rut"), kwargs("usuario_rut"), Nothing)
        Dim usuario_clave As String = If(kwargs.ContainsKey("usuario_clave"), kwargs("usuario_clave"), Nothing)
        If Not String.IsNullOrEmpty(usuario_rut) AndAlso Not String.IsNullOrEmpty(usuario_clave) Then
            Me.auth("pass") = New Dictionary(Of String, String) From {
                {"rut", usuario_rut},
                {"clave", usuario_clave}
            }
        End If
    End Sub

    ''' <summary>
    ''' Obtiene la autenticación de tipo 'pass'.
    ''' </summary>
    ''' <returns>Dictionary(Of String, String) Información de autenticación.</returns>
    ''' <exception cref="ApiException">Si falta información de autenticación.</exception>
    Protected Function GetAuthPass() As Dictionary(Of String, Dictionary(Of String, String))
        If Not Me.auth.ContainsKey("pass") Then
            Throw New ApiException("auth.pass missing.")
        End If
        If Not Me.auth("pass").ContainsKey("rut") Then
            Throw New ApiException("auth.pass.rut missing.")
        End If
        If String.IsNullOrEmpty(Me.auth("pass")("rut")) Then
            Throw New ApiException("auth.pass.rut empty.")
        End If
        If Not Me.auth("pass").ContainsKey("clave") Then
            Throw New ApiException("auth.pass.clave missing.")
        End If
        If String.IsNullOrEmpty(Me.auth("pass")("clave")) Then
            Throw New ApiException("auth.pass.clave empty.")
        End If
        Return Me.auth
    End Function
End Class
