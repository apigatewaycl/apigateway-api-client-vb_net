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
''' Conjunto de pruebas para ActividadesEconomicas
''' </summary>
<TestClass()> Public Class TestActividadesEconomicas

    ''' <summary>
    ''' Pruebas de ActividadesEconomicas que recibirá de parámetro un entero 1
    '''
    ''' Variables:
    ''' test_env: Instancia para inicialización de Variables de entorno
    ''' Actividades: Instancia de ActividadesEconomicas
    ''' listado: Resultado del método ListadoActividades(1)
    ''' 
    ''' Assert: listado >= 0 == true
    ''' AssertFailedException: Si las condiciones no se cumplen
    ''' ApiException: Si otro Error es encontrado
    ''' </summary>
    <TestMethod()> Public Sub TestActividadesPrimera()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim Actividades As New ActividadesEconomicas(Nothing, Nothing, Nothing, True, Nothing)

        Try
            Dim listado As Dictionary(Of String, Object) = Actividades.ListadoActividades(1)
            If (listado.Count = 0) Then
                Trace.WriteLine("La lista de actividades económicas está vacía")
            End If

            For Each elemento In listado
                Trace.WriteLine(elemento.ToString())
            Next elemento

            Assert.AreEqual(listado.Count >= 0, True)

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

    ''' <summary>
    ''' Pruebas de ActividadesEconomicas que recibirá de parámetro un entero 2
    '''
    ''' Variables:
    ''' test_env: Instancia para inicialización de Variables de entorno
    ''' Actividades: Instancia de ActividadesEconomicas
    ''' listado: Resultado del método ListadoActividades(2)
    ''' 
    ''' Assert: listado >= 0 == true
    ''' AssertFailedException: Si las condiciones no se cumplen
    ''' ApiException: Si otro Error es encontrado
    ''' </summary>
    <TestMethod()> Public Sub TestActividadesSegunda()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim Actividades As New ActividadesEconomicas(Nothing, Nothing, Nothing, True, Nothing)

        Try
            Dim listado As Dictionary(Of String, Object) = Actividades.ListadoActividades(2)
            If (listado.Count = 0) Then
                Trace.WriteLine("La lista de actividades económicas está vacía")
            End If

            For Each elemento In listado
                Trace.WriteLine(elemento.ToString())
            Next elemento

            Assert.AreEqual(listado.Count >= 0, True)
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

    ''' <summary>
    ''' Pruebas de ActividadesEconomicas que no recibirá parámetro (default: Nothing)
    '''
    ''' Variables:
    ''' test_env: Instancia para inicialización de Variables de entorno
    ''' Actividades: Instancia de ActividadesEconomicas
    ''' listado: Resultado del método ListadoActividades()
    ''' 
    ''' Assert: listado >= 0 == true
    ''' AssertFailedException: Si las condiciones no se cumplen
    ''' ApiException: Si otro Error es encontrado
    ''' </summary>
    <TestMethod()> Public Sub TestActividadesDefault()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim Actividades As New ActividadesEconomicas(Nothing, Nothing, Nothing, True, Nothing)

        Try
            Dim listado As Dictionary(Of String, Object) = Actividades.ListadoActividades()
            If (listado.Count = 0) Then
                Trace.WriteLine("La lista de actividades económicas está vacía")
            End If

            For Each elemento In listado
                Trace.WriteLine(elemento.ToString())
            Next elemento

            Assert.AreEqual(listado.Count >= 0, True)
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

    ''' <summary>
    ''' Pruebas de ActividadesEconomicas que hará un listado default (Primera categoría)
    '''
    ''' Variables:
    ''' test_env: Instancia para inicialización de Variables de entorno
    ''' Actividades: Instancia de ActividadesEconomicas
    ''' listado: Resultado del método ListadoPrimeraCategoria()
    ''' 
    ''' Assert: listado >= 0 == true
    ''' AssertFailedException: Si las condiciones no se cumplen
    ''' ApiException: Si otro Error es encontrado
    ''' </summary>
    <TestMethod()> Public Sub TestListadoPrimeraCategoria()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim Actividades As New ActividadesEconomicas(Nothing, Nothing, Nothing, True, Nothing)

        Try
            Dim listado As Dictionary(Of String, Object) = Actividades.ListadoPrimeraCategoria()
            If (listado.Count = 0) Then
                Trace.WriteLine("La lista de actividades económicas está vacía")
            End If

            For Each elemento In listado
                Trace.WriteLine(elemento.ToString())
            Next elemento

            Assert.AreEqual(listado.Count >= 0, True)
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

    ''' <summary>
    ''' Pruebas de ActividadesEconomicas que hará un listado default (Segunda categoría)
    '''
    ''' Variables:
    ''' test_env: Instancia para inicialización de Variables de entorno
    ''' Actividades: Instancia de ActividadesEconomicas
    ''' listado: Resultado del método ListadoSegundaCategoria()
    ''' 
    ''' Assert: listado >= 0 == true
    ''' AssertFailedException: Si las condiciones no se cumplen
    ''' ApiException: Si otro Error es encontrado
    ''' </summary>
    <TestMethod()> Public Sub TestListadoSegundaCategoria()
        Dim test_env As New TestEnv
        test_env.SetVariablesDeEntorno()

        Dim Actividades As New ActividadesEconomicas(Nothing, Nothing, Nothing, True, Nothing)

        Try
            Dim listado As Dictionary(Of String, Object) = Actividades.ListadoSegundaCategoria()
            If (listado.Count = 0) Then
                Trace.WriteLine("La lista de actividades económicas está vacía")
            End If

            For Each elemento In listado
                Trace.WriteLine(elemento.ToString())
            Next elemento

            Assert.AreEqual(listado.Count >= 0, True)
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