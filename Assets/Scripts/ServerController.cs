using UnityEngine;
using System.Net;
using System;
using System.IO;
using UnityEngine.UI;
using Assets.Scripts;

public class ServerController : MonoBehaviour
{
    private const string API_KEY = "";
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