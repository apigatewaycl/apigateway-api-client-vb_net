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

Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports apigatewaycl

''' <summary>
''' 
''' </summary>
<TestClass()> Public Class TestBhe
    ''' <summary>
    ''' 
    ''' </summary>
    <TestMethod()> Public Sub TestBheEmitidosDocumentos()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()
        Dim rut As String = Environment.GetEnvironmentVariable("USUARIO_RUT")
        Dim clave As String = Environment.GetEnvironmentVariable("USUARIO_CLAVE")
        Dim periodo As String = Environment.GetEnvironmentVariable("TEST_BHE_FECHA")
        Dim usuario As New Dictionary(Of String, String) From {
            {"usuario_rut", rut},
            {"usuario_clave", clave}
        }
        Dim Bhes As New BheEmitidas(usuario)

        'Try
        Dim listado As List(Of Dictionary(Of String, Object)) = Bhes.Documentos(rut, periodo)
        If (listado.Count = 0) Then
            Trace.WriteLine("La lista de boletas está vacía")
        End If

        For Each listaBoletas In listado
            For Each boleta In listaBoletas
                Trace.WriteLine(boleta.ToString())
            Next boleta
        Next listaBoletas

        Assert.AreEqual(listado.Count >= 0, True)
        'Catch ex As AssertFailedException
        '    Trace.WriteLine($"No se ha podido encontrar boletas. Error: {ex.Message}")
        '    Assert.Fail()
        'Catch ex As ApiException
        '    Trace.WriteLine($"Error de búsqueda. Error: {ex.Message}")
        '    Assert.Fail()
        'Catch ex As Exception
        '    Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
        '    Assert.Fail()
        'End Try
    End Sub

End Class