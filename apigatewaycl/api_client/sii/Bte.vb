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
''' Módulo para la emisión de Boletas de Terceros Electrónicas del SII.
''' 
''' Para más información sobre la API, consulte la `documentación completa de las BHE <href>https://developers.apigateway.cl/#e08f50ab-5509-48ab-81ab-63fc8e5985e1</href> `_.
''' </summary>
Public Class Bte
    Inherits ApiBase

    ''' <summary>
    ''' Provee métodos para emitir, anular, y consultar información relacionada con BTEs.   
    ''' </summary>
    ''' <param name="kwargs">Dictionary(Of String, String) Argumentos adicionales (usuario_rut y usuario_clave).</param>
    Public Sub New(kwargs As Dictionary(Of String, String))
        MyBase.New(kwargs:=kwargs)
    End Sub

    ''' <summary>
    ''' Obtiene los documentos de BTE emitidos por un emisor en un periodo específico.
    ''' </summary>
    ''' <param name="emisor">String RUT del emisor de las BTE.</param>
    ''' <param name="periodo">String Período de tiempo de las BTE emitidas.</param>
    ''' <returns>List(Of Dictionary(Of String, Object)) Respuesta JSON con los documentos BTE.</returns>
    Public Function Documentos(emisor As String, periodo As String) As List(Of Dictionary(Of String, Object))
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }

        Dim response = Me.client.PostMethod($"/sii/bte/emitidas/documentos/{emisor}/{periodo}", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As List(Of Dictionary(Of String, Object)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Obtiene la representación HTML de una BTE emitida.
    ''' </summary>
    ''' <param name="codigo">String Código único de la BTE.</param>
    ''' <returns>String Contenido HTML de la BTE.</returns>
    Public Function Html(codigo As String) As String
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }

        Dim response = Me.client.PostMethod($"/sii/bte/emitidas/html/{codigo}", body)

        Return response.Content.ReadAsStringAsync().Result
    End Function

    ''' <summary>
    ''' Emite una nueva Boleta de Tercero Electrónica.
    ''' </summary>
    ''' <param name="datos">Dictionary(Of String, Object) Datos de la boleta a emitir. </param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la confirmación de la emisión de la BTE.</returns>
    Public Function Emitir(datos As Dictionary(Of String, String)) As Dictionary(Of String, Object)
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()},
            {"boleta", datos}
            }

        Dim response = Me.client.PostMethod($"/sii/bte/emitidas/emitir", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Anula una BTE emitida.
    ''' </summary>
    ''' <param name="emisor">String RUT del emisor de la boleta.</param>
    ''' <param name="numero">String Número de la boleta.</param>
    ''' <param name="causa">Integer Causa de anulación.</param>
    ''' <param name="periodo">String Período de emisión de la boleta (opcional).</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la confirmación de la anulación.</returns>
    Public Function Anular(emisor As String, numero As String, Optional causa As Integer = 3, Optional periodo As String = Nothing) As Dictionary(Of String, Object)
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }
        Dim resource As String = $"/sii/bte/emitidas/anular/{emisor}/{numero}?causa={causa}"

        If String.IsNullOrEmpty(periodo) = False Then
            resource &= $"&periodo={periodo}"
        End If

        Dim response = Me.client.PostMethod(resource, body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Obtiene la tasa de retención aplicada a un receptor por un emisor específico.
    ''' </summary>
    ''' <param name="emisor">String RUT del emisor de la boleta.</param>
    ''' <param name="receptor">String RUT del receptor de la boleta.</param>
    ''' <param name="periodo">String Período de emisión de la boleta (opcional)</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la tasa de retención.</returns>
    Public Function ReceptorTasa(emisor As String, receptor As String, Optional periodo As String = Nothing) As Dictionary(Of String, Object)
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }
        Dim resource As String = $"/sii/bte/emitidas/receptor_tasa/{emisor}/{receptor}"

        If String.IsNullOrEmpty(periodo) = False Then
            resource &= $"?periodo={periodo}"
        End If

        Dim response = Me.client.PostMethod(resource, body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

End Class
