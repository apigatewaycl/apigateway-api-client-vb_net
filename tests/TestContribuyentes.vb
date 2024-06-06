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

<TestClass()> Public Class TestContribuyentes

    <TestMethod()> Public Sub TestSituacionEconomica()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()
        Dim rut As String = Environment.GetEnvironmentVariable("USUARIO_RUT")
        Dim contribuyente As New Contribuyentes()

        Try
            Dim detalleContribuyente As Dictionary(Of String, Object) = contribuyente.SituacionTributaria(rut)

            For Each itemContribuyente In detalleContribuyente
                Trace.WriteLine(itemContribuyente.ToString())
            Next

            Assert.AreEqual(detalleContribuyente.Count >= 0, True)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido encontrar el contribuyente. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As ApiException
            Trace.WriteLine($"Error de búsqueda. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As Exception
            Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
            Assert.Fail()
        End Try
    End Sub

End Class