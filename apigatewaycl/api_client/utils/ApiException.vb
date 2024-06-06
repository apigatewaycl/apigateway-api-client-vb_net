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

Public Class ApiException
    Inherits Exception

    ' Propiedades para el mensaje de error, el código y los parámetros adicionales
    Public Property ErrorMessage As String
    Public Property ErrorCode As Integer?
    Public Property ErrorParams As Dictionary(Of String, Object)

    ''' <summary>
    ''' Excepción personalizada para errores en el cliente de la API.
    ''' </summary>
    ''' <param name="message">String Mensaje de Error.</param>
    ''' <param name="code">Integer Código de Error (opcional).</param>
    ''' <param name="params">Dictionary(Of String, Object) Parámetros adicionales del Error (opcional).</param>
    Public Sub New(message As String, Optional code As Integer? = Nothing, Optional params As Dictionary(Of String, Object) = Nothing)
        MyBase.New(message)
        Me.ErrorMessage = message
        Me.ErrorCode = code
        Me.ErrorParams = params
    End Sub

    ''' <summary>
    ''' Devuelve una representación en cadena del error, proporcionando un contexto claro
    ''' del problema ocurrido. Esta representación incluye el prefijo "[API Gateway]",
    ''' seguido del código de Error si está presente, y el mensaje de Error.
    '''
    ''' Si se especifica un código de Error, el formato será:
    ''' "[API Gateway] Error {code}: {message}"
    '''
    ''' Si no se especifica un código de Error, el formato será:
    ''' "[API Gateway] {message}"
    ''' </summary>
    ''' <returns>Una cadena que representa el Error de una manera clara y concisa.</returns>
    Public Overrides Function ToString() As String
        If ErrorCode.HasValue Then
            Return $"[API Gateway] Error {ErrorCode}: {ErrorMessage}"
        Else
            Return $"[API Gateway] {ErrorMessage}"
        End If
    End Function
End Class
