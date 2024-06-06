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
Imports System.Net.Http

''' <summary>
''' Módulo para obtener las actividades económicas del SII.
''' 
''' Para más información sobre la API, consulte la `documentación completa de Actividades Económicas <href>https://developers.apigateway.cl/#e64eb128-173a-48c7-ab0b-b6152e59c327</href> `_.
''' </summary>
Public Class ActividadesEconomicas
    Inherits ApiBase

    ''' <summary>
    ''' Cliente específico para interactuar con los endpoints de actividades económicas de la API de API Gateway.
    ''' Provee métodos para obtener listados de actividades económicas, tanto de primera como de segunda categoría.
    ''' </summary>
    ''' <param name="api_token">Token de autenticación para la API.</param>
    ''' <param name="api_url">URL base para la API.</param>
    ''' <param name="api_version">Versión de la API.</param>
    ''' <param name="api_raise_for_status">Si se debe lanzar una excepción automáticamente para respuestas de error HTTP. Por defecto es True.</param>
    ''' <param name="kwargs">Argumentos adicionales para la autenticación.</param>
    Public Sub New(Optional api_token As String = Nothing, Optional api_url As String = Nothing, Optional api_version As String = Nothing, Optional api_raise_for_status As Boolean = True, Optional kwargs As Dictionary(Of String, String) = Nothing)
        MyBase.New(api_token, api_url, api_version, api_raise_for_status, kwargs)
    End Sub

    ''' <summary>
    ''' Obtiene un listado de actividades económicas. Puede filtrar por categoría.
    ''' </summary>
    ''' <param name="categoria">Categoría de las actividades económicas (opcional).</param>
    ''' <returns>Respuesta JSON con el listado de actividades económicas.</returns>
    Public Function ListadoActividades(Optional categoria As Integer? = Nothing) As Dictionary(Of String, Object)
        Dim url As String = "/sii/contribuyentes/actividades_economicas"
        If categoria IsNot Nothing Then
            url &= $"/{categoria}"
        End If
        Dim response = Me.client.GetMethod(url)
        Dim jsonResponse = response.Content.ReadAsStringAsync().Result
        Dim result As Dictionary(Of String, Object) = JsonConvert.DeserializeObject(Of Dictionary(Of String, Object))(jsonResponse)

        Return result
    End Function

    ''' <summary>
    ''' Obtiene un listado de actividades económicas de primera categoría.
    ''' </summary>
    ''' <returns>Dictionary Respuesta JSON con el listado de actividades económicas de primera categoría.</returns>
    Public Function ListadoPrimeraCategoria() As Dictionary(Of String, Object)
        Return ListadoActividades(1)
    End Function

    ''' <summary>
    ''' Obtiene un listado de actividades económicas de segunda categoría.
    ''' </summary>
    ''' <returns>Dictionary Respuesta JSON con el listado de actividades económicas de segunda categoría.</returns>
    Public Function ListadoSegundaCategoria() As Dictionary(Of String, Object)
        Return ListadoActividades(2)
    End Function
End Class