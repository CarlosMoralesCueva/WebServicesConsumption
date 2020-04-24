## Programa desarrollado en Unity que muestra el estado de los servidores por región del juego League of Legends


### Pasos previos 

* Instalar la versión 2019.3.7f1 de Unity
* Si se desea hacer debug en el teléfono Android descargar de Play Store Unity Remote 5
* Obtener la llave de desarrollo para el acceso al API de League of Legends en la siguiente página: https://developer.riotgames.com/

### Creación del programa

1. Iniciar Unity Hub y crear un nuevo programa 3D llamado WebServicesConsumption
2. Ir a File, Build Settings ...
3. Seleccionar Android, dar clic en Switch Platform
4. En la pestaña Game cambiar la resolución de pestaña a 1280x720 Portrait
5. Dar clic derecho en la escena, UI, Dropdown
6. Darle nombre: Server_list, posicionarlo: Pos Y: 350, Width: 160, Height: 30, Scale X: 4, Y: 4; en "options" ingresar el siguiente listado: 
* LAN
* LAS
* Brasil
* Norteamérica
* Korea
* Japón
* Europa Occ.
* Europa del Norte
* Oceanía
* Rusia
* Turquía
7. Dar clic derecho en la escena, UI, Button
8. Darle nombre: Obtain_information, posicionarla: Pos Y: 230, Width: 160, Height: 30, Scale X: 4, Y: 4; como texto colocar: Obtener información
9. Ingresar 6 cuadros de texto dando clic derecho en la escena, UI, Text y configurar cada cuadro de la siguiente manera:
* Nombre: Game, Pos X: -200, Pos Y: -350, Width: 250, Height: 75, Texto: Game, Font Size: 48 
* Nombre: Store, Pos X: -25, Pos Y: -350, Width: 250, Height: 75, Texto: Store, Font Size: 48 
* Nombre: WebSite, Pos X: 125, Pos Y: -350, Width: 250, Height: 75, Texto: WebSite, Font Size: 48 
* Nombre: Client, Pos X: 325, Pos Y: -350, Width: 250, Height: 75, Texto: Client, Font Size: 48 
* Nombre: Server, Pos X: -100, Pos Y: 120, Width: 500, Height: 75, Texto: (quitar texto), Font Size: 48 
* Nombre: Slug, Pos X: 380, Pos Y: 120, Width: 250, Height: 75, Texto: (quitar texto), Font Size: 48 
10. En la parte inferior, en Assets dar clic derecho y crear la carpeta Images
11. Copiar las 10 imágenes de los servidores a esta carpeta
12. Ingresar 10 imágenes dando clic derecho en la escena, UI, Raw Image y configurar de la siguiente manera cada imagen:
* Nombre: LAN_image: LA1
* Nombre: LAS_image: LA2
* Nombre: BR_image: BR1
* Nombre: NA_image: NA1
* Nombre: JP_image: JP1
* Nombre: EUW_image: EUW1
* Nombre: EUN_image: EUN1
* Nombre: OC_image: OC1
* Nombre: RU_image: RU1
* Nombre: TR_image: TR1
13. Deshabilitar todas las imágenes y en el recuadro de Texture arrastrar la imagen correspondiente
14. En la parte inferior, en Assets dar clic derecho y crear la carpeta Scripts
15. En esta carpeta crear un script de C# con el nombre de ServerController
16. Copiar el siguiente código en el script
```c#
using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.UI;

public class ServerController : MonoBehaviour
{
    private const string API_KEY = TuLlaveDeDesarrolladorVaAqui;
    public Dropdown serverList;
    public Text game;
    public Text store;
    public Text website;
    public Text client;
    public Text serverName;
    public Text slug;
    public RawImage lan;
    public RawImage las;
    public RawImage na;
    public RawImage br;
    public RawImage euw;
    public RawImage eun;
    public RawImage jp;
    public RawImage oc;
    public RawImage ru;
    public RawImage tr;

    public void GetServersInfo()
    {
	// Obtiene el nombre del servidor según la opción
	// escogida en el dropdown de servidores
        string serverNameCode = GetServersName(serverList.value);

	// Realiza la consulta al API e instancia la información
	// en la clase, muestra la imagen del servidor seleccionado
        ServerInfo serverInfo = CallApi(serverNameCode);

	// Asigna nombres a los campos de texto
        serverName.text = serverInfo.name;
        slug.text = serverInfo.slug.ToUpper();

	// Si los servidores están funcionando cambia el color
	// de letra a verde
        foreach (var service in serverInfo.services)
        {
            if (service.name == "Game")
                game.color = service.status == "online" ? Color.green : Color.red;

            if (service.name == "Store")
                store.color = service.status == "online" ? Color.green : Color.red;

            if (service.name == "Website")
                website.color = service.status == "online" ? Color.green : Color.red;

            if (service.name == "Client")
                client.color = service.status == "online" ? Color.green : Color.red;
        }
    }

    private string GetServersName(int serverInternalCode)
    {
        string serverNameCode = string.Empty;

        switch (serverInternalCode)
        {
            case 0:
                serverNameCode = "LA1";
                lan.gameObject.SetActive(true);
                break;
            case 1:
                serverNameCode = "LA2";
                las.gameObject.SetActive(true);
                break;
            case 2:
                serverNameCode = "BR1";
                br.gameObject.SetActive(true);
                break;
            case 3:
                serverNameCode = "NA1";
                na.gameObject.SetActive(true);
                break;
            case 4:
                serverNameCode = "KR";
                lan.gameObject.SetActive(false);
                las.gameObject.SetActive(false);
                oc.gameObject.SetActive(false);
                ru.gameObject.SetActive(false);
                tr.gameObject.SetActive(false);
                euw.gameObject.SetActive(false);
                eun.gameObject.SetActive(false);
                br.gameObject.SetActive(false);
                jp.gameObject.SetActive(false);
                na.gameObject.SetActive(false);
                break;
            case 5:
                serverNameCode = "JP1";
                jp.gameObject.SetActive(true);
                break;
            case 6:
                serverNameCode = "EUW1";
                euw.gameObject.SetActive(true);
                break;
            case 7:
                serverNameCode = "EUN1";
                eun.gameObject.SetActive(true);
                break;
            case 8:
                serverNameCode = "OC1";
                oc.gameObject.SetActive(true);
                break;
            case 9:
                serverNameCode = "RU";
                ru.gameObject.SetActive(true);
                break;
            case 10:
                serverNameCode = "TR1";
                tr.gameObject.SetActive(true);
                break;
        }

        return serverNameCode;
    }

    private ServerInfo CallApi(string serverName)
    {
        ServerInfo info = new ServerInfo();
        HttpWebRequest request = (HttpWebRequest)WebRequest.Create(String.Format("https://{0}.api.riotgames.com/lol/status/v3/shard-data?api_key={1}", serverName, API_KEY));
        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        StreamReader reader = new StreamReader(response.GetResponseStream());
        string jsonResponse = reader.ReadToEnd();
        Debug.Log(jsonResponse);
        JsonUtility.FromJsonOverwrite(jsonResponse, info);

        return info;
    }
}
```
17. En la carpeta Scripts crear una clase de C# con el nombre de ServerInfo
18. Copiar el siguiente código en el script
```c#
using System;
using System.Collections.Generic;

namespace Assets.Scripts
{
    [Serializable]
    public class ServerInfo
    {
        public string name;

        public string slug;

        public string hostname;

        public string region_tag;

        public List<string> locales;

        public List<Service> services;
    }

    [Serializable]
    public class Service
    {
        public string name;

        public string slug;

        public string status;

        public List<Incident> incidents;
    }

    [Serializable]
    public class Incident
    {
        public bool active;

        public string created_at;

        public long id;

        public List<Message> updates;
    }

    [Serializable]
    public class Message
    {
        public string severity;

        public string updated_at;

        public string author;

        public List<Translation> translations;

        public string created_at;

        public string id;

        public string content;  
    }

    [Serializable]
    public class Translation
    {
        public string locale;

        public string updated_at;

        public string content;
    }
}
```
19. Una vez copiado el código añadir el script ServerController al botón Obtain_information
20. Asociar la función GetServersInfo a la funcionalidad On Click() del botón seleccionado
21. Asociar cada elemento de la interfaz gráfica a su correspondiente elemento del script 
22. Dar clic  en "Add Open Scenes" y Build
23. Probar
