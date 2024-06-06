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
''' Módulo para obtener indicadores desde el SII.
''' 
''' Para más información sobre la API, consulte la `documentación completa de los Indicadores <href>://developers.apigateway.cl/#65aa568c-4c5a-448b-9f3b-95c3d9153e4d</href> `_.
''' </summary>
Public Class Uf
    Inherits ApiBase

    ''' <summary>
    ''' Cliente específico para interactuar con los endpoints de valores de UF (Unidad de Fomento) de la API de API Gateway.
    ''' 
    ''' Provee métodos para obtener valores de UF anuales, mensuales y diarios.
    ''' </summary>
    Public Sub New()

    End Sub

    ''' <summary>
    ''' Obtiene los valores de la UF para un año específico.
    ''' </summary>
    ''' <param name="anio">Integer Año para el cual se quieren obtener los valores de la UF.</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con los valores de la UF del año especificado.</returns>
    Public Function Anual(anio As Integer) As Dictionary(Of String, Object)
        Dim anioString As String = anio.ToString()
        Dim response = Me.client.GetMethod($"/sii/indicadores/uf/anual/{anio}")
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return If(result.ContainsKey(anioString), result, New Dictionary(Of String, Object))
    End Function

    ''' <summary>
    ''' Obtiene los valores de la UF para un mes específico.
    ''' </summary>
    ''' <param name="periodo">String Período en formato AAAAMM (año y mes).</param>
    ''' <returns>Dictionary(Of String, Object) Respuesta JSON con los valores de la UF del mes especificado.</returns>
    Public Function Mensual(periodo As String) As Dictionary(Of String, Object)
        Dim anio As String = periodo.Substring(0, 4)
        Dim mes As String = periodo.Substring(4, 2)
        Dim response = Me.client.GetMethod($"/sii/indicadores/uf/anual/{anio}/{mes}")
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return If(result.ContainsKey(periodo), result, New Dictionary(Of String, Object))
    End Function

    ''' <summary>
    ''' Obtiene el valor de la UF para un día específico.
    ''' </summary>
    ''' <param name="fecha">String Fecha en formato AAAA-MM-DD.</param>
    ''' <returns>Single Valor de la UF para el día especificado.</returns>
    Public Function Diario(fecha As String) As Single
        Dim anio As String = fecha.Split("-")(0)
        Dim mes As String = fecha.Split("-")(1)
        Dim dia As String = fecha.Split("-")(2)
        Dim response = Me.client.GetMethod($"/sii/indicadores/uf/anual/{anio}/{mes}/{dia}")
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim datos As Dictionary(Of String, Single) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Single))(jsonResponse)
        Dim indice As String = $"{anio}{mes}{dia}"

        Return If(datos.ContainsKey(indice), datos(indice), 0.0F)
    End Function

End Class
