# DEFENSA ORAL GRUPAL - TP 12 INTEGRACIÓN
## Sistema de Gestión de Combis - Terminal Obelisco

---

## INTEGRANTES DEL GRUPO

| Nombre Completo | Rol en la Presentación |
|-----------------|------------------------|
| **María Escalante** | Persona 1 - Introducción y Análisis |
| **Ignacio Mondragón** | Persona 2 - Estructura del Código |
| **Joaquín Alcalaya** | Persona 3 - Funcionalidades Parte 1 |
| **Franco Quevedo** | Persona 4 - Funcionalidades Parte 2 |
| **Gonzalo Quarchioni** | Persona 5 - Decisiones Técnicas y Pruebas |
| **Martiniano Cigliutti** | Persona 6 - Demostración y Cierre |

**Asignatura:** Programación y Estructuras de Datos  
**Universidad:** UAI - Universidad Abierta Interamericana  
**Trabajo Práctico:** TP12 - Integración de Contenidos  
**Fecha:** Noviembre 2024

---

## DISTRIBUCIÓN DE TIEMPOS

| Persona | Secciones | Tiempo Estimado |
|---------|-----------|-----------------|
| María Escalante | Introducción + Análisis del Problema | 4-5 min |
| Ignacio Mondragón | Estructura del Código | 3-4 min |
| Joaquín Alcalaya | Funcionalidades Parte 1 (Pasajeros, Timer, Rutas) | 3-4 min |
| Franco Quevedo | Funcionalidades Parte 2 (Estadísticas, Persistencia, Reportes) | 3-4 min |
| Gonzalo Quarchioni | Decisiones Técnicas + Pruebas + Desafíos | 4-5 min |
| Martiniano Cigliutti | Demostración en Vivo + Conclusiones | 5-6 min |
| **TOTAL** | | **22-28 minutos** |

---

# PERSONA 1: MARÍA ESCALANTE
## Introducción y Análisis del Problema (4-5 minutos)

### INTRODUCCIÓN (1-2 minutos)

**[María habla]**

Buenas tardes, profesor/a. Somos el equipo conformado por María Escalante, Ignacio Mondragón, Joaquín Alcalaya, Franco Quevedo, Gonzalo Quarchioni y Martiniano Cigliutti. Hoy vamos a presentar nuestro Trabajo Práctico N°12 que consiste en un sistema de gestión para un servicio de combis.

El sistema maneja la fila de espera de pasajeros en la Terminal Obelisco y permite gestionar viajes hacia diferentes destinos de Buenos Aires. Lo desarrollamos en C# usando Windows Forms y .NET 8.

El objetivo principal era aplicar las estructuras de datos que vimos en la cursada, específicamente **colas (Queue)**, para simular una situación real: una fila de personas esperando para subir a una combi.

---

### ANÁLISIS DEL PROBLEMA (2-3 minutos)

**[María continúa]**

Antes de empezar a programar, analizamos el problema en detalle. La pregunta clave era: **¿Qué estructura de datos debemos usar?**

#### ¿Por qué elegimos una Cola (Queue)?

Esta fue nuestra primera decisión técnica importante. Analizamos el problema y nos dimos cuenta de que una fila de espera en la vida real funciona con el principio **FIFO - First In, First Out**.

**¿Qué significa esto?**  
Que el primer pasajero que llega es el primero que sube a la combi. Esto es justo y es exactamente cómo funciona una Cola.

**¿Por qué NO usamos una Pila (Stack)?**  
Una Pila funciona con LIFO - Last In, First Out. Esto significa que el último en llegar sería el primero en subir, lo cual sería totalmente injusto para los que esperaron más tiempo.

*[Mostrar en pantalla]*

```csharp
// Cola para la fila de pasajeros (FIFO)
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();
```

#### Comparación de Estructuras

| Estructura | Principio | ¿Apropiada? | Motivo |
|------------|-----------|-------------|--------|
| **Queue** | FIFO | ? SÍ | Primer llegado, primero atendido (justo) |
| **Stack** | LIFO | ? NO | Último llegado, primero atendido (injusto) |
| **List** | Indexada | ? Tal vez | Requiere manejo manual de índices |

Como ven, la Cola era la opción natural y correcta para este problema.

**[Transición]** Ahora le paso la palabra a Ignacio para que explique cómo estructuramos el código.

---

# PERSONA 2: IGNACIO MONDRAGÓN
## Estructura del Código (3-4 minutos)

**[Ignacio habla]**

Gracias María. Voy a explicar cómo organizamos el código del proyecto.

### Arquitectura del Sistema

Nuestro sistema sigue una arquitectura de tres capas:

```
???????????????????????????????????????
?   CAPA DE PRESENTACIÓN              ?
?   (Windows Forms - Form1)           ?
???????????????????????????????????????
?   CAPA DE LÓGICA DE NEGOCIO         ?
?   (Validaciones y Cálculos)         ?
???????????????????????????????????????
?   CAPA DE DATOS                     ?
?   (Pasajero, EstadisticasDiarias)   ?
???????????????????????????????????????
```

### Clases Principales

Dividimos el sistema en 3 clases principales:

#### 1. Clase Pasajero (Modelo de datos)

Esta clase representa a cada pasajero que se anota. Tiene:
- **Nombre:** El nombre del pasajero
- **Tipo:** Si es Normal, Estudiante o Jubilado
- **HoraAnotacion:** Cuándo se anotó (para calcular tiempo de espera)
- **Tarifa:** Una propiedad calculada automáticamente

*[Mostrar código]*

```csharp
public decimal Tarifa
{
    get
    {
        return Tipo switch
        {
            TipoPasajero.Normal => 500m,
            TipoPasajero.Estudiante => 250m,
            TipoPasajero.Jubilado => 0m,
            _ => 500m
        };
    }
}
```

Lo importante acá es que la tarifa **se calcula automáticamente**. No la guardamos, la calculamos cada vez que la necesitamos. Esto evita inconsistencias en los datos.

#### 2. Clase EstadisticasDiarias

Esta clase lleva el registro de:
- Total de viajes del día
- Total de pasajeros transportados
- Recaudación total
- Desglose por tipo de pasajero

También mantiene una lista de todos los viajes realizados para poder generar el reporte al final del día.

#### 3. Form1 (Lógica principal)

Esta es la clase más grande y compleja. Acá está toda la lógica de:
- Anotar pasajeros
- Iniciar viajes
- Manejar el temporizador
- Guardar y cargar datos
- Generar reportes

### Estructuras Dinámicas Utilizadas

Utilizamos tres estructuras dinámicas:

```csharp
// Cola para la fila de espera
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();

// Lista para los pasajeros en la combi actualmente
private List<Pasajero> pasajerosEnCombi = new List<Pasajero>();

// Lista para el historial de viajes
public List<Viaje> Viajes { get; set; }
```

Todas estas estructuras **crecen dinámicamente** según la necesidad. No usamos arrays de tamaño fijo.

**[Transición]** Ahora le paso la palabra a Joaquín para que explique las funcionalidades principales.

---

# PERSONA 3: JOAQUÍN ALCALAYA
## Funcionalidades Parte 1 (3-4 minutos)

**[Joaquín habla]**

Gracias Ignacio. Yo voy a explicar las primeras tres funcionalidades implementadas.

### 1. Gestión de Pasajeros

Esta es la funcionalidad base del sistema. Cuando el usuario clickea "Anotar":

1. **Validamos** que haya escrito un nombre
2. **Verificamos** que la combi no esté llena (19 lugares máximo)
3. **Creamos** el objeto Pasajero con el tipo seleccionado
4. Lo **agregamos a la cola** con `Enqueue()`
5. **Actualizamos** la lista en pantalla

*[Mostrar código resumido]*

```csharp
private void btnAnotar_Click(object sender, EventArgs e)
{
    // 1. Validar nombre
    if (string.IsNullOrEmpty(nombrePasajero)) return;
    
    // 2. Verificar capacidad
    if (filaDeEspera.Count >= 19) return;
    
    // 3. Crear y agregar
    Pasajero nuevoPasajero = new Pasajero(nombrePasajero, tipo);
    filaDeEspera.Enqueue(nuevoPasajero);
    
    // 4. Iniciar timer si es el primero
    if (filaDeEspera.Count == 1)
        IniciarTemporizador();
}
```

### 2. Temporizador Automático (20 minutos)

Esta fue una de las partes más interesantes del proyecto. Cuando se anota el primer pasajero:

- Se activa un temporizador de 20 minutos (1200 segundos)
- Cada segundo se actualiza el contador en pantalla
- El color cambia según el tiempo restante:
  - **Azul:** Más de 5 minutos
  - **Naranja:** Entre 1 y 5 minutos
  - **Rojo:** Menos de 1 minuto
- Si llega a 00:00, **la combi parte automáticamente**

*[Mostrar código]*

```csharp
private void timerCombi_Tick(object sender, EventArgs e)
{
    tiempoRestanteSegundos--;
    ActualizarTiempoRestante();
    
    if (tiempoRestanteSegundos <= 0)
    {
        timerCombi.Stop();
        btnSubir_Click(sender, e); // Viaje automático
    }
}
```

El cambio de color proporciona retroalimentación visual al usuario sobre cuánto tiempo queda.

### 3. Tres Rutas Diferentes

Implementamos 3 rutas que el usuario puede elegir al iniciar un viaje:

- **Ruta 1:** Obelisco ? Puerto Madero (20 min)
- **Ruta 2:** Obelisco ? Recoleta (25 min)
- **Ruta 3:** Obelisco ? Palermo (30 min)

Cuando inicia un viaje, se abre un formulario modal con RadioButtons para elegir la ruta. La Ruta 1 viene seleccionada por defecto.

*[Si es posible, mostrar screenshot del selector de rutas]*

**[Transición]** Ahora Franco va a continuar con las demás funcionalidades.

---

# PERSONA 4: FRANCO QUEVEDO
## Funcionalidades Parte 2 (3-4 minutos)

**[Franco habla]**

Gracias Joaquín. Yo voy a explicar las funcionalidades de estadísticas, persistencia y reportes.

### 4. Estadísticas en Tiempo Real

El sistema muestra en tiempo real, en un panel a la derecha:
- Cantidad de viajes realizados en el día
- Total de pasajeros transportados
- Recaudación total del día

Estas estadísticas se actualizan automáticamente después de cada viaje.

```csharp
private void ActualizarEstadisticas()
{
    lblViajesHoy.Text = $"Viajes: {estadisticas.TotalViajes}";
    lblPasajerosHoy.Text = $"Pasajeros: {estadisticas.TotalPasajeros}";
    lblRecaudacion.Text = $"Recaudacion:\n${estadisticas.RecaudacionTotal:N2}";
}
```

### 5. Persistencia de Datos

Esta es una de las funcionalidades más importantes. Implementamos persistencia usando dos archivos de texto en formato CSV:

**Archivo 1: fila_espera.txt**  
Guarda los pasajeros que están esperando:

```
Juan Perez|0|2025-01-13 08:15:30
Ana Garcia|1|2025-01-13 08:17:45
Luis Lopez|2|2025-01-13 08:20:12
```

Formato: `Nombre|TipoNumerico|FechaHora`

**Archivo 2: estadisticas.txt**  
Guarda las estadísticas del día:

```
Fecha|2025-01-13
TotalViajes|5
TotalPasajeros|73
RecaudacionTotal|28250.00
```

#### Característica Destacada: Restauración del Temporizador

Lo más interesante de la persistencia es que cuando cierras y volvés a abrir la aplicación:
- Se cargan todos los pasajeros que estaban esperando
- El temporizador **continúa desde donde quedó**, considerando el tiempo transcurrido

*[Mostrar código]*

```csharp
var primerPasajero = filaDeEspera.Peek();
int tiempoTranscurrido = 
    (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
tiempoRestanteSegundos = 
    Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);
```

Por ejemplo:
- Primer pasajero anotado: 08:15:30
- Aplicación cerrada y reabierta: 08:20:00 (4 min 30 seg después)
- Tiempo restante: 20:00 - 04:30 = **15:30**

### 6. Generación de Reportes

Al final del día se puede generar un reporte completo en formato .txt que incluye:

- **Resumen general:** Viajes, pasajeros, recaudación
- **Desglose por tipo:** Con porcentajes calculados
- **Recaudación por tipo:** Cuánto aportó cada categoría
- **Detalle de cada viaje:** Hora, pasajeros, recaudación
- **Promedios:** Pasajeros por viaje, recaudación por viaje

El archivo se genera con fecha y hora en el nombre, y se abre automáticamente en Notepad.

**[Transición]** Ahora Gonzalo va a explicar las decisiones técnicas y las pruebas realizadas.

---

# PERSONA 5: GONZALO QUARCHIONI
## Decisiones Técnicas, Pruebas y Desafíos (4-5 minutos)

**[Gonzalo habla]**

Gracias Franco. Yo voy a explicar las decisiones técnicas que tomamos y las pruebas que realizamos.

### Decisiones Técnicas

#### 1. Estructuras Dinámicas

Decidimos usar estructuras dinámicas en lugar de arrays de tamaño fijo porque:

- **Queue<Pasajero>**: Crece automáticamente según los pasajeros que se anotan
- **List<Pasajero>**: Flexible para almacenar temporalmente pasajeros
- **List<Viaje>**: Para el historial sin límite predefinido

Esto hace que el sistema sea más flexible y escalable.

#### 2. Validaciones Implementadas

Implementamos validaciones robustas en todos los puntos críticos:

1. **Campo vacío:** No dejamos anotar sin nombre
2. **Capacidad máxima:** No dejamos anotar más de 19 pasajeros
3. **Mínimo de pasajeros:** Verificamos que haya al menos 1 para partir
4. **Confirmación de cierre:** Si hay pasajeros esperando, pedimos confirmación

#### 3. Uso de LINQ

Usamos LINQ para hacer consultas eficientes sobre las colecciones:

```csharp
// Contar pasajeros de un tipo específico
int normales = pasajerosEnCombi.Count(p => p.Tipo == Pasajero.TipoPasajero.Normal);

// Calcular recaudación total
decimal recaudacion = pasajerosEnCombi.Sum(p => p.Tarifa);
```

LINQ nos permite escribir código más limpio y legible.

### Pruebas Realizadas

Probamos exhaustivamente el sistema con 5 casos principales:

#### Caso 1: Flujo Normal
- Anotar 3 pasajeros de diferentes tipos
- Iniciar viaje manualmente
- **Resultado:** ? Recaudación correcta ($750)

#### Caso 2: Temporizador Automático
- Anotar 1 pasajero y esperar 20 minutos
- **Resultado:** ? La combi parte automáticamente

#### Caso 3: Persistencia Completa
- Anotar pasajeros, cerrar, esperar, reabrir
- **Resultado:** ? Pasajeros y temporizador restaurados correctamente

#### Caso 4: Capacidad Máxima
- Anotar 19 pasajeros e intentar agregar el #20
- **Resultado:** ? Mensaje "Combi llena", no se agrega

#### Caso 5: Reportes
- Realizar varios viajes y generar reporte
- **Resultado:** ? Todos los cálculos correctos

**Cobertura:** 100% de las funcionalidades fueron probadas exitosamente.

### Desafíos y Soluciones

Durante el desarrollo enfrentamos tres desafíos principales:

#### Desafío 1: Recorrer Queue sin modificarla

**Problema:** Necesitábamos mostrar los pasajeros en el ListBox pero `foreach` directamente en Queue los extrae.

**Solución:** Usar `ToArray()` para crear una copia temporal:
```csharp
foreach (Pasajero pasajero in filaDeEspera.ToArray())
{
    lstEnEspera.Items.Add($"{posicion}. {pasajero}");
}
```

#### Desafío 2: Persistencia del Temporizador

**Problema:** Al cerrar y reabrir, el temporizador no consideraba el tiempo transcurrido.

**Solución:** Calcular la diferencia entre `DateTime.Now` y `HoraAnotacion` del primer pasajero.

#### Desafío 3: Formato del Reporte

**Problema:** Generar un reporte legible y bien formateado.

**Solución:** Usar `StringBuilder` y construir el texto línea por línea con encabezados claros.

### Interfaz de Usuario

Diseñamos la interfaz pensando en la usabilidad:

- **Colores significativos:**
  - Azul: Elementos normales
  - Verde: Botón de iniciar viaje (acción positiva)
  - Naranja: Botón de reporte (información)
  - Rojo: Cerrar y temporizador urgente

- **Símbolos ASCII** en lugar de emojis: `[N]`, `[E]`, `[J]`
  - Garantiza compatibilidad en cualquier sistema

**[Transición]** Ahora Martiniano va a hacer la demostración en vivo del sistema.

---

# PERSONA 6: MARTINIANO CIGLIUTTI
## Demostración en Vivo y Cierre (5-6 minutos)

**[Martiniano habla]**

Gracias Gonzalo. Ahora voy a mostrarles el sistema funcionando en tiempo real.

### Demostración en Vivo

**Paso 1: Pantalla Inicial**

*[Abrir la aplicación]*

"Como ven, acá está la pantalla principal. Arriba está el título 'SERVICIO DE COMBIS - Terminal Obelisco', a la izquierda tenemos el panel donde anotamos pasajeros, y a la derecha están las estadísticas del día."

**Paso 2: Anotar Pasajeros**

*[Anotar 3 pasajeros]*

"Voy a anotar 3 pasajeros de diferentes tipos para mostrar cómo funciona..."

- "Primero anoto a Juan Pérez como Normal..."
  *[Escribir nombre, seleccionar Normal, click Anotar]*
  "Ven que aparece en la lista y el temporizador arranca en 20:00."

- "Ahora María García como Estudiante..."
  *[Repetir proceso]*
  "Aparece con el símbolo [E] y la tarifa $250."

- "Y Luis López como Jubilado..."
  *[Repetir proceso]*
  "Aparece con [J] y tarifa $0 porque viaja gratis."

**Paso 3: Observar el Temporizador**

*[Señalar el temporizador]*

"Ven que el temporizador va bajando cada segundo. Está en azul porque quedan más de 5 minutos. Si esperamos, cambiaría a naranja y después a rojo. Si llega a 00:00, la combi sale automáticamente."

**Paso 4: Iniciar Viaje**

*[Click en "Subir a la combi"]*

"Ahora inicio el viaje. Me pide confirmación..."
*[Click Yes]*

"Y acá me muestra el selector de rutas con las 3 opciones disponibles."
*[Seleccionar Ruta 1, Click Confirmar]*

"Y acá me muestra todos los detalles del viaje:"
- "Los 3 pasajeros que subieron"
- "El desglose por tipo: 1 Normal, 1 Estudiante, 1 Jubilado"
- "La recaudación total: $750"
*[Click OK]*

**Paso 5: Mostrar Estadísticas**

*[Señalar panel de estadísticas]*

"Ven que las estadísticas se actualizaron automáticamente:"
- "Viajes: 1"
- "Pasajeros: 3"
- "Recaudación: $750.00"

**Paso 6: Generar Reporte**

*[Click en "Generar Reporte Del Dia"]*

"Ahora genero el reporte del día..."
*[Click Yes para abrir]*

"Se abre automáticamente en Notepad con toda la información:"
- "Resumen general"
- "Desglose por tipo con porcentajes"
- "Recaudación por tipo"
- "Detalle del viaje que hicimos"

*[Cerrar Notepad]*

**Paso 7: Demostrar Persistencia**

*[Anotar 2 pasajeros más]*

"Voy a anotar 2 pasajeros más rápidamente..."
*[Anotar "Pedro" y "Laura"]*

"Ahora cierro la aplicación..."
*[Cerrar con X]*

"Me pregunta si estoy seguro porque hay pasajeros esperando..."
*[Click Yes]*

"Y ahora la vuelvo a abrir..."
*[Reabrir aplicación]*

"Ven que los 2 pasajeros siguen ahí en la lista, y el temporizador continúa desde donde quedó. No se reinició, sino que calculó el tiempo que pasó mientras estaba cerrada."

### Conclusiones del Equipo

**[Martiniano continúa]**

Para finalizar, este proyecto nos permitió aplicar de forma práctica todo lo que aprendimos sobre estructuras de datos durante la cursada.

#### Objetivos Cumplidos

Como equipo logramos:

? Implementar correctamente una Cola para la fila de espera  
? Aplicar el principio FIFO de forma efectiva  
? Desarrollar una interfaz intuitiva y funcional  
? Implementar validaciones robustas  
? Agregar funcionalidades extra como temporizador, estadísticas y reportes  
? Documentar el código de forma clara  
? Crear un sistema escalable y fácil de mantener

#### Aprendizajes Principales

Como grupo aprendimos:

1. **Estructuras de datos:** Ahora entendemos perfectamente cuándo usar Queue, Stack o List
2. **Persistencia:** Cómo guardar y cargar datos de forma eficiente
3. **Trabajo en equipo:** Cómo dividir tareas y integrar el código
4. **Eventos y temporizadores:** Manejo avanzado en Windows Forms
5. **LINQ:** Lo útil que es para consultas sobre colecciones
6. **Buenas prácticas:** Validaciones, manejo de errores, código limpio

#### Conocimientos Aplicados

En este TP aplicamos:

**De la cursada:**
- Colas (Queue) - FIFO
- Listas (List) - Estructuras dinámicas
- Archivos - StreamWriter/StreamReader
- Validaciones

**Adicionales:**
- POO completa
- Enumeraciones
- LINQ
- Eventos
- Temporizadores
- Serialización CSV personalizada

#### Posibles Mejoras Futuras

Si tuviéramos más tiempo, agregaríamos:
- Base de datos SQL en lugar de archivos
- Múltiples terminales simultáneas
- Sistema de reservas anticipadas
- Gráficos de estadísticas
- Exportación de reportes a PDF

### Cierre Final

**[Martiniano cierra]**

El código completo está disponible en GitHub en el repositorio:  
**https://github.com/ignaciomondragon24/tp12**

Toda la documentación también está disponible allí.

En nombre de todo el equipo: María, Ignacio, Joaquín, Franco, Gonzalo y yo, Martiniano, muchas gracias por su atención.

Quedamos a disposición para cualquier pregunta que tengan.

---

## PREGUNTAS FRECUENTES (TODO EL EQUIPO)

*[Cada integrante puede responder según su área]*

### Pregunta: ¿Por qué Queue y no otra estructura?

**[María o Ignacio responden]**

Porque una fila de espera en la vida real es FIFO. El primero que llega es el primero que se atiende. Si hubiéramos usado Stack (LIFO), el último en llegar sería el primero en subir, lo cual no tiene sentido en este contexto.

### Pregunta: ¿Cómo manejaron la persistencia?

**[Franco responde]**

Usamos dos archivos de texto en formato CSV:
- `fila_espera.txt`: Pasajeros en espera con su tipo y hora
- `estadisticas.txt`: Estadísticas del día

Al cerrar guardamos todo, y al abrir verificamos si los archivos existen y cargamos los datos. Para las estadísticas, además verificamos que sean del día actual.

### Pregunta: ¿Qué pasa si se corta la luz?

**[Gonzalo responde]**

Los datos se guardan solo al cerrar la aplicación normalmente. Si se corta la luz, se perderían los pasajeros en espera en ese momento. Una mejora sería implementar auto-guardado cada cierto tiempo, pero no lo incluimos para mantener el código simple.

### Pregunta: ¿Cómo calcularon las tarifas?

**[Ignacio responde]**

Usamos una propiedad calculada en la clase Pasajero. No guardamos la tarifa, la calculamos on-demand usando un `switch expression`. Esto es más eficiente y evita inconsistencias.

### Pregunta: ¿Cómo se dividieron el trabajo?

**[Martiniano responde]**

Trabajamos de forma colaborativa:
- María: Análisis y diseño inicial
- Ignacio: Estructura de clases y arquitectura
- Joaquín: Funcionalidades de gestión de pasajeros
- Franco: Persistencia y reportes
- Gonzalo: Validaciones y pruebas
- Martiniano: Integración final y documentación

Usamos Git para el control de versiones y nos reunimos regularmente para integrar el código.

---

## NOTAS PARA LA DEFENSA GRUPAL

### Coordinación entre Integrantes:

1. **Antes de la presentación:**
   - Reunirse para practicar al menos 2 veces
   - Cada uno debe conocer su parte perfectamente
   - Ensayar las transiciones entre personas
   - Designar quién responde qué tipo de preguntas

2. **Durante la presentación:**
   - Mantener contacto visual con el profesor y el resto del equipo
   - No interrumpir al compañero que está hablando
   - Estar atentos para las transiciones suaves
   - Tener la aplicación lista para la demo

3. **Transiciones sugeridas:**
   - "Ahora le paso la palabra a [Nombre]"
   - "Gracias [Nombre]. Yo voy a continuar con..."
   - Usar gestos naturales para indicar el cambio

### Timing Individual:

| Persona | Tiempo Ideal | Tiempo Máximo |
|---------|-------------|---------------|
| María | 4-5 min | 6 min |
| Ignacio | 3-4 min | 5 min |
| Joaquín | 3-4 min | 5 min |
| Franco | 3-4 min | 5 min |
| Gonzalo | 4-5 min | 6 min |
| Martiniano | 5-6 min | 8 min |
| **TOTAL** | **22-28 min** | **35 min MAX** |

### Distribución de Preguntas:

- **Estructuras de datos:** María o Ignacio
- **Código y clases:** Ignacio o Joaquín
- **Funcionalidades:** Joaquín o Franco
- **Persistencia:** Franco
- **Validaciones y pruebas:** Gonzalo
- **Integración general:** Martiniano

### Material Necesario:

- [ ] Laptop con Visual Studio abierto
- [ ] Proyecto compilando sin errores
- [ ] Aplicación lista para demostrar
- [ ] PowerPoint con diapositivas de apoyo (opcional)
- [ ] Este documento impreso como guía
- [ ] Agua para cada integrante

### Checklist Pre-Defensa:

- [ ] Todos conocen su parte
- [ ] Practicaron al menos 2 veces juntos
- [ ] La aplicación funciona perfectamente
- [ ] Tienen respuestas preparadas para preguntas comunes
- [ ] Saben quién responde cada tipo de pregunta
- [ ] Llegaron 15 minutos antes
- [ ] Están relajados y confiados

---

**¡MUCHA SUERTE AL EQUIPO! ??**

**María, Ignacio, Joaquín, Franco, Gonzalo y Martiniano:**  
Ustedes hicieron este trabajo juntos, lo conocen perfectamente, y lo van a defender muy bien.

**Recuerden:** Respiren hondo, confíen en ustedes mismos y en sus compañeros.  
**¡Adelante equipo! ??**
