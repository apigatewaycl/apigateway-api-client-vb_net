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
''' Módulo para interactuar con Boletas de Honorarios Electrónicas, tanto emitidas como recibidas, del SII.
''' 
''' Para más información sobre la API, consulte la `documentación completa de las BHE <href>https://developers.apigateway.cl/#4df9775f-2cd3-4b35-80a5-373f2501230c</href> `_.
''' </summary>
Public Class BheEmitidas
    Inherits ApiBase

    ' Constantes Código de Retención
    Public Const RETENCION_RECEPTOR As Integer = 1
    Public Const RETENCION_EMISOR As Integer = 2
    ' Constantes Código de Anulación
    Public Const ANULACION_CAUSA_SIN_PAGO As Integer = 1
    Public Const ANULACION_CAUSA_SIN_PRESTACION As Integer = 2
    Public Const ANULACION_CAUSA_ERROR_DIGITACION As Integer = 3

    ''' <summary>
    ''' Cliente específico para gestionar Boletas de Honorarios Electrónicas (BHE) emitidas.
    ''' 
    ''' Provee métodos para emitir, anular, y consultar información relacionada con BHEs.
    ''' </summary>
    ''' <param name="kwargs">Dictionary(Of String, String) Argumentos adicionales (usuario_rut y usuario_clave).</param>
    Public Sub New(kwargs As Dictionary(Of String, String))
        MyBase.New(kwargs:=kwargs)
    End Sub

    ''' <summary>
    ''' Obtiene los documentos de BHE emitidos por un emisor en un periodo específico.
    ''' </summary>
    ''' <param name="emisor">String RUT del emisor de las boletas.</param>
    ''' <param name="periodo">String Período de tiempo de las boletas emitidas, formato AAAAMM.</param>
    ''' <returns>List(Of Dictionary(Of String, Object)) Respuesta JSON con los documentos de BHE.</returns>
    Public Function Documentos(emisor As String, periodo As String) As List(Of Dictionary(Of String, Object))
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
        }

        Dim response = Me.client.PostMethod($"/sii/bhe/emitidas/documentos/{emisor}/{periodo}?auth_cache=0", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As List(Of Dictionary(Of String, Object)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Emite una nueva Boleta de Honorarios Electrónica.
    ''' </summary>
    ''' <param name="Boleta">Dictionary(Of String, Object) Información detallada de la boleta a emitir.</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la confirmación de la emisión de la BHE.</returns>
    Public Function Emitir(Boleta As Dictionary(Of String, String)) As Dictionary(Of String, Object)
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()},
            {"boleta", Boleta}
            }

        Dim response = Me.client.PostMethod("/sii/bhe/emitidas/emitir", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Obtiene el PDF de una BHE emitida.
    ''' </summary>
    ''' <param name="codigo">String Código único de la BHE.</param>
    ''' <returns>Contenido del PDF de la BHE.</returns>
    Public Function Pdf(codigo As String) As Byte()
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }

        Dim response = Me.client.PostMethod($"/sii/bhe/emitidas/pdf/{codigo}", body)


        Return response.Content.ReadAsByteArrayAsync().Result
    End Function

    ''' <summary>
    ''' Envía por correo electrónico una BHE emitida.
    ''' </summary>
    ''' <param name="codigo">String Código único de la BHE a enviar.</param>
    ''' <param name="correo">String Dirección de correo electrónico a la cual enviar la BHE.</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con la confirmación del envío del email.</returns>
    Public Function Email(codigo As String, correo As String) As Dictionary(Of String, Object)
        Dim emailDict As New Dictionary(Of String, String) From {
            {"email", correo}
        }
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()},
            {"destinatario", emailDict}
            }

        Dim response = Me.client.PostMethod($"/sii/bhe/emitidas/email/{codigo}", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

    Public Function Anular(emisor As String, folio As String, Optional causa As Integer = ANULACION_CAUSA_ERROR_DIGITACION)
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }

        Dim response = Me.client.PostMethod($"/sii/bhe/emitidas/anular/{emisor}/{folio}?causa={causa}", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

End Class

Public Class BheRecibidas
    Inherits ApiBase

    ''' <summary>
    ''' Cliente específico para gestionar Boletas de Honorarios Electrónicas (BHE) emitidas.
    ''' 
    ''' Provee métodos para emitir, anular, y consultar información relacionada con BHEs.
    ''' </summary>
    ''' <param name="kwargs">Dictionary Argumentos adicionales (usuario_rut y usuario_clave).</param>
    Public Sub New(kwargs As Dictionary(Of String, String))
        MyBase.New(kwargs:=kwargs)
    End Sub

    ''' <summary>
    ''' Obtiene los documentos de BHE recibidos por un receptor en un periodo específico.
    ''' </summary>
    ''' <param name="receptor">String RUT del receptor de las boletas.</param>
    ''' <param name="periodo">String Período de tiempo de las boletas recibidas.</param>
    ''' <param name="pagina">Integer Número de página para paginación (opcional).</param>
    ''' <param name="paginaSigCodigo">String Código para la siguiente página (opcional).</param>
    ''' <returns>List(Of Dictionary(Of String, Object)) Respuesta JSON con los documentos de BHE.</returns>
    Public Function Documentos(receptor As String, periodo As String, Optional pagina As Integer = 0, Optional paginaSigCodigo As String = Nothing) As List(Of Dictionary(Of String, Object))
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }
        Dim url As String = $"/sii/bhe/recibidas/documentos/{receptor}/{periodo}"

        If pagina > 0 Then
            'Dim codigo As String = 
            url &= $"?pagina={pagina}&pagina_sig_codigo={If(paginaSigCodigo, "00000000000000")}"
        End If

        Dim response = Me.client.PostMethod(url, body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As List(Of Dictionary(Of String, Object)) = JsonConvert.DeserializeObject(Of List(Of Dictionary(Of String, Object)))(jsonResponse)

        Return result
    End Function


    ''' <summary>
    ''' Obtiene el PDF de una BHE emitida.
    ''' </summary>
    ''' <param name="codigo">String Código único de la BHE.</param>
    ''' <returns>Contenido del PDF de la BHE.</returns>
    Public Function Pdf(codigo As String) As Byte()
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }

        Dim response = Me.client.PostMethod($"/sii/bhe/recibidas/pdf/{codigo}", body)


        Return response.Content.ReadAsByteArrayAsync().Result
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="emisor"></param>
    ''' <param name="numero"></param>
    ''' <param name="causa"></param>
    ''' <returns></returns>
    Public Function Observar(emisor As String, numero As String, Optional causa As Integer = 1) As Dictionary(Of String, Object)
        Dim body As New Dictionary(Of String, Object) From {
            {"auth", Me.GetAuthPass()}
            }

        Dim response = Me.client.PostMethod($"/sii/bhe/recibidas/observar/{emisor}/{numero}?causa={causa}", body)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

End Class