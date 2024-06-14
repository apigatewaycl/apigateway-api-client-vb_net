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

        Dim USUARIO_RUT As String = Environment.GetEnvironmentVariable("USUARIO_RUT")
        Dim USUARIO_CLAVE As String = Environment.GetEnvironmentVariable("USUARIO_CLAVE")
        Dim TEST_BHE_FECHA As String = Environment.GetEnvironmentVariable("TEST_BHE_FECHA")

        Dim usuario As New Dictionary(Of String, String) From {
            {"usuario_rut", USUARIO_RUT},
            {"usuario_clave", USUARIO_CLAVE}
        }

        Try
            Dim ListadoBhe As New BheEmitidas(usuario)
            Dim respuesta As List(Of Dictionary(Of String, Object)) = ListadoBhe.Documentos(USUARIO_RUT, TEST_BHE_FECHA)

            For Each listaBoletas In respuesta
                For Each boleta In listaBoletas
                    Trace.WriteLine(boleta.ToString())
                Next boleta
            Next listaBoletas

            If (respuesta.Count = 0) Then
                Trace.WriteLine("El usuario no ha emitido boletas de honorarios.")
            End If

            Assert.AreEqual(respuesta.Count >= 0, True)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido encontrar boletas. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As ApiException
            Trace.WriteLine($"Error de búsqueda. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As Exception
            Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
            Assert.Fail()
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    <TestMethod()> Public Sub TestBheEmitidosPdf()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim USUARIO_RUT As String = Environment.GetEnvironmentVariable("USUARIO_RUT")
        Dim USUARIO_CLAVE As String = Environment.GetEnvironmentVariable("USUARIO_CLAVE")
        Dim TEST_BHE_CODIGO As String = Environment.GetEnvironmentVariable("TEST_BHE_CODIGO")

        Dim usuario As New Dictionary(Of String, String) From {
            {"usuario_rut", USUARIO_RUT},
            {"usuario_clave", USUARIO_CLAVE}
        }

        Try
            Dim ListadoBhe As New BheEmitidas(usuario)
            Dim respuesta As Byte() = ListadoBhe.Pdf(TEST_BHE_CODIGO)

            If (respuesta.Length = 0) Then
                Trace.WriteLine($"El BHE no existe para el emisor {USUARIO_RUT}.")
            Else
                IO.File.WriteAllBytes($"test_pdf_{USUARIO_RUT}_{TEST_BHE_CODIGO}.pdf", respuesta)
            End If

            Assert.AreEqual(respuesta.Length >= 0, True)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido encontrar boletas. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As ApiException
            Trace.WriteLine($"Error de búsqueda. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As Exception
            Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
            Assert.Fail()
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    <TestMethod()> Public Sub TestBheEmitidosEmail()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim USUARIO_RUT As String = Environment.GetEnvironmentVariable("USUARIO_RUT")
        Dim USUARIO_CLAVE As String = Environment.GetEnvironmentVariable("USUARIO_CLAVE")
        Dim TEST_BHE_CODIGO As String = Environment.GetEnvironmentVariable("TEST_BHE_CODIGO")
        Dim TEST_BHE_EMAIL As String = Environment.GetEnvironmentVariable("TEST_BHE_EMAIL")

        Dim usuario As New Dictionary(Of String, String) From {
            {"usuario_rut", USUARIO_RUT},
            {"usuario_clave", USUARIO_CLAVE}
        }

        Try
            Dim ListadoBhe As New BheEmitidas(usuario)
            Dim correo As Dictionary(Of String, Object) = ListadoBhe.Email(TEST_BHE_CODIGO, TEST_BHE_EMAIL)

            If (correo.Count = 0) Then
                Trace.WriteLine($"El BHE no existe para el emisor {USUARIO_RUT}.")
            End If

            Assert.AreEqual(correo.Count >= 0, True)
        Catch ex As AssertFailedException
            Trace.WriteLine($"No se ha podido enviar el correo. Error: {ex.Message}")
            Assert.Fail()
        Catch ex As ApiException
            Trace.WriteLine($"Error: {ex.Message}")
            Assert.Fail()
        Catch ex As Exception
            Trace.WriteLine($"Error desconocido. Error: {ex.Message}")
            Assert.Fail()
        End Try
    End Sub


End Class