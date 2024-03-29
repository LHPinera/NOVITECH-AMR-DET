# NOVITECH-AMR-DET
## Aplicación para lectura de placas con cámaras LPR
### Autor: Luis Humberto Piñera Coronado
### Versión 1

### Este programa recibe los eventos de cámaras LPR.

### Flujo de la solución

<p>Se crea una lista de cámaras definidas en la tabla Config_CAM de la base de datos, esta incluye: </p>
      
- Marca
- Dirección IP
- Puerto
- Nombre de usuario
- Contraseña
<p>Ya creada la lista se abre un hilo por cada camara que responda a la solicitud de conexion y se activa un evento de respuesta</p>
<p>Cuando se recibe el evento de respuesta se valida la marca de la cámara y se envia la respuesta al sdk correspondiente </p>
<p>El sdk analiza el evento y crea una estructura de datos que contiene: </p>

- Placa
- Imágen de la placa
- Imágen del vehiculo
- Fecha y hora del evento
- Id de la cámara
- Velocidad (cuando la cámara tenga la funcionalidad)

<p>Se valida el numero de placa para que al menos contenga una letra y un numero</p>
<p>Se hace la insercion en la tabla Hits de la base de datos</p>

#### La solucion consta de 4 modulos.

##### 1.- App.Data
<p>Aqui se definen los parámetros de conexion al servidor SQL y el proceso de inserción de los hits en la base de datos 

| Programa | Descripción |
| --- | --- |
| cdAlerta.cs | Define el proceso de la insercion de datos |

| Función | Descripción |
| --- | --- |
| Insertar | Funcion general de insercion de hit de placa, el programa puede hacer la insercion de manera local o en la nube o en ambas |
| InsertarNube | Hace la inserción del registro en el servidor remoto |
| InsertarLocal | Hace ña inserción del registro en el servidor local |
| InsertDb | Valida los registro locales y los envia a la nube |
| UpdateLocal | Aactualiza el estatus del registro local que fue enviado a la nube |
| GetCamNube | Crea una lista de cámaras desde el servidor remoto |
| GetCamLocal | Crea una lista de cámaras desde el servidor local |
| InsertarConfiguracion | Agrega una cámara a la base de datos local |
| GetConfig | Obtiene los parametros de conexion y tipo de camara desde el servidor local |

| Programa | Descripción |
| --- | --- |
| Conection.cs | Define la cadena de conexion para los servicios locales y remotos |

| Función | Descripción |
| --- | --- |
| GetConLocal | Define la cadena de conexion local |
| GetConNube | Define la cadena de conexion remota |
      
#### 2.- App.Deal
<p>Define el procesamiento de los hits recibidos de las cámaras

| Programa | Descripción |
| --- | --- |
| cdAlertas.cs | Realiza la validacion de los datos obtenidos del evento de lectura de las camaras y hace la insercion en la base de datos |
  
| Función | Descripción |
| --- | --- |  
| Actualizar | Ejecuta el proceso de insercion en la base de datos |
| Insertar | Proceso de insercion del hit de la placa, previa validacion del texto obtenido |
| ValidarPlaca | Valida la longitud del texto y verifica los número y letras |
| RevisarNumero | Valida que por lo menos haya un número |
| RevisarLetra | Valida qe por lo menos haya una letra |
| GetCamara | Obtiene la lista de cámaras |
| GetMacAddress | Obtiene la direccion mac local |
| InsertarConfigs | Agrega una nueva camara |
| GetConfig | Obtiene la lista de cámaras |
      
#### 3.- App.Entity
<p>Clases de los datos utilizados por el programa

| Clase | Descripción |
| --- | --- |
| CeAlerta | Datos del evento de lectura (hit) |
| CeAlertaL | Evento de lectura local |
| CeAlertaN | Evento de lectura remoto |
| CeAntena | Datos de lectura de antena - no se utiliza |
| CeCamara | Datos de configuracion de las camaras |
| CeConfig | Datos de las cámaras |
| CeInfoCamara | Datos de conexion de las camaras |
| Lista | Lista de camaras |
 
#### 4.- ANPR Detector
Es la rutina principal de la solucion
- Programas:
   - MainWindow.xml - Es un formulario xml para el despliegue de los datos obtenidos de cada evento

