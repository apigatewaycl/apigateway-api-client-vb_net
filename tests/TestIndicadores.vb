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
Imports System.Globalization

<TestClass()> Public Class TestIndicadores

    <TestMethod()> Public Sub TestIndicadorAnual()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()
        Dim anio = Environment.GetEnvironmentVariable("TEST_UF_ANIO")
        Dim indicador As New Uf()

        Try
            Dim uf As Dictionary(Of String, Object) = indicador.Anual(Convert.ToInt32(anio))
            For Each mes In uf
                Trace.WriteLine(mes.ToString())
            Next

            Assert.AreEqual(uf.ContainsKey(anio), True)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido encontrar el listado. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As ApiException
            Trace.WriteLine($"Error de búsqueda. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As Exception
            Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
            Assert.Fail()
        End Try
    End Sub

    <TestMethod()> Public Sub TestIndicadorMensual()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()
        Dim mes As String = Environment.GetEnvironmentVariable("TEST_UF_MES")
        Dim indicador As New Uf()

        Try
            Dim uf As Dictionary(Of String, Object) = indicador.Mensual(mes)
            For Each dia In uf
                Trace.WriteLine(dia.ToString())
            Next

            Assert.AreEqual(uf.ContainsKey(mes), True)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido encontrar el listado. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As ApiException
            Trace.WriteLine($"Error de búsqueda. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As Exception
            Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
            Assert.Fail()
        End Try
    End Sub

    <TestMethod()> Public Sub TestIndicadorDiario()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()
        Dim fecha = Environment.GetEnvironmentVariable("TEST_UF_FECHA")
        Dim valorUf As Single = Convert.ToSingle(Environment.GetEnvironmentVariable("TEST_UF_VALOR"))
        Dim indicador As New Uf()

        Try
            Dim uf As Single = indicador.Diario(fecha)

            Trace.WriteLine(uf.ToString())

            Assert.AreEqual(uf, valorUf)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido encontrar el listado. Error: {ex.Message}")
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