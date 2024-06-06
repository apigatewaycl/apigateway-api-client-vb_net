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
''' Módulo para interactuar con las opciones de Documentos Tributarios Electrónicos (DTE) del SII.
''' 
''' Para más información sobre la API, consulte la `documentación completa de los DTE <href>https://developers.apigateway.cl/#8c113b9a-ea05-4981-9273-73e3f20ef991</href> `_.
''' </summary>
Public Class DteContribuyentes
    Inherits ApiBase

    ''' <summary>
    ''' Cliente específico para interactuar con los endpoints de contribuyentes de la API de API Gateway.
    ''' 
    ''' Proporciona métodos para consultar la autorización de emisión de DTE de un contribuyente.
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Verifica si un contribuyente está autorizado para emitir DTE.
    ''' </summary>
    ''' <param name="rut">String RUT del contribuyente a verificar.</param>
    ''' <param name="certificacion">Boolean Indica si se consulta en ambiente de certificación (opcional).</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con el estado de autorización del contribuyente.</returns>
    Public Function Autorizacion(rut As String, Optional certificacion As Boolean = False) As Dictionary(Of String, Object)
        Dim certificacionFlag = If(certificacion, 1, 0)
        Dim response = Me.client.GetMethod($"/sii/dte/contribuyentes/autorizado/{rut}?certificacion={certificacionFlag}")
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

End Class

Public Class DteEmitidos
    Inherits ApiBase

    ''' <summary>
    ''' Cliente específico para gestionar DTE emitidos.
    ''' 
    ''' Permite verificar la validez y autenticidad de un DTE emitido.
    ''' </summary>
    ''' <param name="kwargs">Dictionary(Of String, String) Argumentos adicionales (usuario_rut y usuario_clave).</param>
    Public Sub New(kwargs As Dictionary(Of String, String))
        MyBase.New(Nothing, Nothing, Nothing, True, kwargs)
    End Sub

    ''' <summary>
    ''' Verifica la validez de un DTE emitido.
    ''' </summary>
    ''' <param name="emisor">String RUT del emisor del DTE.</param>
    ''' <param name="receptor">String RUT del receptor del DTE.</param>
    ''' <param name="dte">Integer Tipo de DTE.</param>
    ''' <param name="folio">Integer Número de folio del DTE.</param>
    ''' <param name="fecha">String Fecha de emisión del DTE.</param>
    ''' <param name="total">String Monto total del DTE.</param>
    ''' <param name="firma">String Firma electrónica del DTE (opcional).</param>
    ''' <param name="certificacion">Boolean Indica si la verificación es en ambiente de certificación (opcional).</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con el resultado de la verificación del DTE.</returns>
    Public Function Verificar(emisor As String, receptor As String, dte As Integer, folio As Integer, fecha As String, total As Integer, Optional firma As String = Nothing, Optional certificacion As Boolean = False) As Dictionary(Of String, Object)
        Dim dictDte As New Dictionary(Of String, Object) From {
            {"emisor", emisor},
            {"receptor", receptor},
            {"dte", dte},
            {"folio", folio},
            {"fecha", fecha},
            {"total", total},
            {"firma", firma}
        }
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()},
            {"dte", dictDte}
        }

        Dim certificacionFlag = If(certificacion, 1, 0)
        Dim response = Me.client.PostMethod($"/sii/dte/emitidos/verificar?certificacion={certificacionFlag}", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result

    End Function

End Class