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

Imports Newtonsoft.Json

''' <summary>
''' Módulo para obtener los datos a través del SII.
''' 
''' Para más información sobre la API, consulte la `documentación completa de los Contribuyentes <href>https://developers.apigateway.cl/#c88f90b6-36bb-4dc2-ba93-6e418ff42098</href> `_.
''' </summary>
Public Class Contribuyentes
    Inherits ApiBase

    ''' <summary>
    ''' Cliente específico para interactuar con los endpoints de contribuyentes de la API de API Gateway.
    ''' 
    ''' Hereda de ApiBase y utiliza su funcionalidad para realizar solicitudes a la API.
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Obtiene la situación tributaria de un contribuyente.
    ''' </summary>
    ''' <param name="rut">String RUT del contribuyente.</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la situación tributaria del contribuyente.</returns>
    Public Function SituacionTributaria(rut As String) As Dictionary(Of String, Object)
        Dim response = Me.client.GetMethod($"/sii/contribuyentes/situacion_tributaria/tercero/{rut}")
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Verifica el RUT de un contribuyente.
    ''' </summary>
    ''' <param name="serie">String Serie del RUT a verificar.</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la verificación del RUT.</returns>
    Public Function VerificarRut(serie As String) As Dictionary(Of String, Object)
        Dim response = Me.client.GetMethod($"/sii/contribuyentes/rut/verificar/{serie}")
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result

    End Function

End Class
