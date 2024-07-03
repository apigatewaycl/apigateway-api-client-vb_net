# API Gateway: Cliente de API en VB.NET

Enlaces sujetos a cambios.

[![NuGet version](https://img.shields.io/nuget/v/apigatewayclvb.svg)](https://www.nuget.org/packages/apigatewayclvb/)
[![NuGet downloads](https://img.shields.io/nuget/dt/apigatewayclvb.svg)](https://www.nuget.org/packages/apigatewayclcvb/)

Cliente para realizar la integración con los servicios web de [API Gateway](https://www.apigateway.cl) desde Visual Basic.NET.

## Instalación y actualización

### Instalación mediante el Administrador de Paquetes NuGet

1. Abre tu proyecto en Visual Studio.
2. Haz clic derecho en el proyecto en el Explorador de Soluciones y selecciona "Administrar paquetes NuGet...".
3. En la pestaña "Examinar", busca `apigatewaycl`. Debe ser la versión Visual Basic.NET (vb, vbnet, o vb.net).
4. Selecciona el paquete `apigatewaycl` y haz clic en "Instalar".

### Instalación desde la línea de comandos (cmd)

1. Abre la línea de comandos desde Herramientas, Administrador de paquetes NuGet, Consola del administrador de paquetes.
2. Ejecuta el siguiente comando para instalar `apigatewaycl`:

```sh
nuget install apigatewayclvb
```

### Cliente genérico vs clientes específicos
Este cliente de API Gateway tiene 2 formas de acceder a los recursos de la API:

Cliente genérico: es un cliente que permite consumir de manera sencilla cualquier recurso de la API, que actualmente exista o sea añadido en el futuro. Esto se logra porque el cliente recibe los nombres de los recursos, la parte de la URL que permite acceder al servicio web solicitado. Se proveen métodos que sólo sirven para acceder a la API de manera genérica, pero no para hacer acciones específicas ni obtener los datos en un formato específico. Este cliente es el que entrega mayor flexibilidad, ya que cada programador decide qué recursos desea consumir y cómo desea obtener los datos del mismo.

Clientes específicos: son clases que permiten acceder de forma más natural a los recursos de la API. Al instanciar la clase, se tendrán métodos sencillos con parámetros para consumir la API; sin ser necesario preocuparse de recordar o buscar en la documentación el nombre de los recursos que se deben consumir. Además de entregar los datos ya "listos" para ser usados en vez de tener que preocuparse de qué método del cliente genérico usar para obtenerlos en el formato requerido.

### Autenticación en API Gateway
Lo más simple, y recomendado, es usar una variable de entorno con el [token del usuario](https://apigateway.cl/dashboard#api-auth), la cual será reconocida automáticamente por el cliente:

```sh
export APIGATEWAY_API_TOKEN="aquí-tu-token-de-usuario"
```

Si no se desea usar una variable de entorno, al instanciar los objetos se deberá indicar el token del usuario. Ejemplo con el cliente genérico:

```VB

    Imports apigatewaycl

    Dim APIGATEWAY_API_TOKEN = "aquí-tu-token-de-usuario"
    Environment.SetEnvironmentVariable("APIGATEWAY_API_TOKEN", APIGATEWAY_API_TOKEN)
    Dim client = New ApiClient(token:=APIGATEWAY_API_TOKEN)
```

El siguiente es un ejemplo con el cliente específico para BHE. Primero se pasan
los datos obligatorios de RUT y clave del usuario. Luego además se pasa el token
del usuario de la API.

```C#

    Imports System.Collections.Generic
    Imports apigatewaycl.api_client.sii


    Dim APIGATEWAY_API_TOKEN As String = "aquí-tu-token-de-usuario"
    Environment.SetEnvironmentVariable("APIGATEWAY_API_TOKEN", APIGATEWAY_API_TOKEN)
    Dim SII_USUARIO_RUT As String = "rut_sii"
    Dim SII_USUARIO_CLAVE As String = "clave_sii"
    Dim usuario As new Dictionary(Of String, String) From
    {
        { "usuario_rut", SII_USUARIO_RUT },
        { "usuario_clave", SII_USUARIO_CLAVE }
    }
    Dim ListadoBhe As New BheEmitidas(usuario)
```

Si se usan variables de entorno, en ambos ejemplos se puede omitir el argumento api_token.

### Licencia
--------

Este programa es software libre: usted puede redistribuirlo y/o modificarlo
bajo los términos de la GNU Lesser General Public License (LGPL) publicada
por la Fundación para el Software Libre, ya sea la versión 3 de la Licencia,
o (a su elección) cualquier versión posterior de la misma.

Este programa se distribuye con la esperanza de que sea útil, pero SIN
GARANTÍA ALGUNA; ni siquiera la garantía implícita MERCANTIL o de APTITUD
PARA UN PROPÓSITO DETERMINADO. Consulte los detalles de la GNU Lesser General
Public License (LGPL) para obtener una información más detallada.

Debería haber recibido una copia de la GNU Lesser General Public License
(LGPL) junto a este programa. En caso contrario, consulte
[GNU Lesser General Public License](http://www.gnu.org/licenses/lgpl.html).

Enlaces
-------

- [Sitio web API Gateway](https://www.apigateway.cl).
- [Código fuente en GitHub](https://github.com/apigatewaycl/apigateway-api-client-vb6).
- [Paquete en NuGet](https://www.nuget.org/packages/apigatewayclvb).
- [Documentación en Read the Docs](https://apigatewaycl.readthedocs.io/es/latest).
