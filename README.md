# EPSAS
Buen dia, 

# RECOMENDACIONES 

1. El proyecto esta creado en .NET CORE 8. cuando se ejectute por primera ves el proyecto se demorara un poco ya que cuando la data no existe en la DB el proyecto leera el archivo excel que esta en la carpeta Files, y asi generara las inserciones autamaticas a la base de datos 
2. tener tambien en cuenta que si se quiere ejecutar en local hay que cambiar la cadena de conexion que esta en el AppSettings.json
3. El unico codigo que se debe ejecutar en la Base de datos es:
    #Create database EPSA;
  
