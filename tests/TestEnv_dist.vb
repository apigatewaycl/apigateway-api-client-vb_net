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

Public Class TestEnv_dist

    Public Sub New()

    End Sub

    Public Sub SetVariablesDeEntorno()
        ' Variables de ambiente de API Gateway
        Dim APIGATEWAY_API_TOKEN As String = ""
        Dim APIGATEWAY_API_URL As String = "https://apigateway.cl"

        ' Variables de Contribuyente de primera categoría
        Dim USUARIO_RUT As String = ""
        Dim USUARIO_CLAVE As String = ""

        ' Variables de BHE
        Dim TEST_BHE_FECHA As String = "202405"
        Dim TEST_BHE_CODIGO As String = ""
        Dim TEST_BHE_EMAIL As String = ""

        ' Variables de Indicador UF
        Dim TEST_UF_FECHA As String = "2024-05-17"
        Dim TEST_UF_MES As String = "202405"
        Dim TEST_UF_ANIO As String = "2024"
        Dim TEST_UF_VALOR As Single = 37354.68F

        ' Asignación de Variables de Entorno
        Environment.SetEnvironmentVariable("APIGATEWAY_API_TOKEN", APIGATEWAY_API_TOKEN)
        Environment.SetEnvironmentVariable("APIGATEWAY_API_URL", APIGATEWAY_API_URL)

        Environment.SetEnvironmentVariable("USUARIO_RUT", USUARIO_RUT)
        Environment.SetEnvironmentVariable("USUARIO_CLAVE", USUARIO_CLAVE)

        Environment.SetEnvironmentVariable("TEST_BHE_FECHA", TEST_BHE_FECHA)
        Environment.SetEnvironmentVariable("TEST_BHE_CODIGO", TEST_BHE_CODIGO)
        Environment.SetEnvironmentVariable("TEST_BHE_EMAIL", TEST_BHE_EMAIL)

        Environment.SetEnvironmentVariable("TEST_UF_FECHA", TEST_UF_FECHA)
        Environment.SetEnvironmentVariable("TEST_UF_MES", TEST_UF_MES)
        Environment.SetEnvironmentVariable("TEST_UF_ANIO", TEST_UF_ANIO)
        Environment.SetEnvironmentVariable("TEST_UF_VALOR", TEST_UF_VALOR.ToString())

    End Sub
End Class
