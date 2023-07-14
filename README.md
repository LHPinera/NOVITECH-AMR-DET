# NOVITECH-AMR-DET
## Aplicación para lectura de placas con cámaras LPR
### Autor: Luis Humberto Piñera Coronado
### Versión 1

### Este programa recibe los eventos de cámaras LPR.

#### La solucion consta de 4 modulos.
##### 1.- App.Data
Aqui se definen los parámetros de conexion al servidor SQL y el proceso de inserción de los hits en la base de datos 
- Programas:
  -  cdAlerta.cs - Define el proceso de la insercion de datos, sus funciones son:
 
| Función ! Descripción |
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
    - Conection.cs - Define la cadena de conexion para los servicios locales y remotos, sus funciones son:
| Función ! Descripción |
| --- | --- |
| GetConLocal | Define la cadena de conexion local |
| GetConNube | Define la cadena de conexion remota |
      
#### 2.- App.Deal
Define el procesamiento de los hits recibidos de las cámaras

| Función | Descripción |
| --- | --- |
| Actualizar | Ejecuta el proceso de insercion en la base de datos |
| Insertar | Proceso de insercion del hit de la placa, previa validacion del texto obtenido |

- Programas:
    - cdAlertas.cs - Realiza la validacion de los datos obtenidos del evento de lectura de las camaras y hace la insercion en la base de datos sus funciones son:
  
  
                                              
       - Actualizar - Ejecuta el proceso de insercion en la base de datos
       - Insertar - Proceso de insercion del hit de la placa, previa validacion del texto obtenido
       - ValidarPlaca - Valida la longitud del texto y verifica los número y letras
       - RevisarNumero - Valida que por lo menos haya un número
       - RevisarLetra - Valida qe por lo menos haya una letra
       - GetCamara - Obtiene la lista de cámaras
       - GetMacAddress - Obtiene la direccion mac local
       - InsertarConfigs - Agrega una nueva camara
       - GetConfig - Obtiene la lista de cámaras
      
#### 3.- App.Entity
Clases de los datos utilizados por el programa
- Clases
   - CeAlerta - Datos del evento de lectura (hit)
   - CeAlertaL - Evento de lectura local
   - CeAlertaN - Evento de lectura remoto
   - CeAntena - Datos de lectura de antena - no se utiliza
   - CeCamara - Datos de configuracion de las camaras
   - CeConfig - Datos de las cámaras
   - CeInfoCamara - Datos de conexion de las camaras
   - Lista - Lista de camaras
 
#### 4.- ANPR Detector
Es la rutina principal de la solucion
- Programas:
   - MainWindow.xml - Es un formulario xml para el despliegue de los datos obtenidos de cada evento

