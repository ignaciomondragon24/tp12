# DEFENSA ORAL - TP 12 INTEGRACIÓN
## Sistema de Gestión de Combis - Terminal Obelisco

**Alumno:** [Tu Nombre]  
**Legajo:** [Tu Legajo]  
**Materia:** Programación y Estructuras de Datos  
**Universidad:** UAI - Universidad Abierta Interamericana

---

## ?? INTRODUCCIÓN (1-2 minutos)

Buenas tardes, profesor/a. Hoy voy a presentar mi Trabajo Práctico N°12 que consiste en un sistema de gestión para un servicio de combis.

El sistema maneja la fila de espera de pasajeros en la Terminal Obelisco y permite gestionar viajes hacia diferentes destinos de Buenos Aires. Lo desarrollé en C# usando Windows Forms y .NET 8.

El objetivo principal era aplicar las estructuras de datos que vimos en la cursada, específicamente **colas (Queue)**, para simular una situación real: una fila de personas esperando para subir a una combi.

---

## ?? PARTE 1: ANÁLISIS DEL PROBLEMA (2-3 minutos)

### ¿Por qué elegí una Cola (Queue)?

Esta fue mi primera decisión importante. Analicé el problema y me di cuenta de que una fila de espera en la vida real funciona con el principio **FIFO - First In, First Out**.

**¿Qué significa esto?**  
Que el primer pasajero que llega es el primero que sube a la combi. Esto es justo y es exactamente cómo funciona una Cola.

**¿Por qué NO usé una Pila (Stack)?**  
Una Pila funciona con LIFO - Last In, First Out. Esto significa que el último en llegar sería el primero en subir, lo cual sería totalmente injusto para los que esperaron más tiempo.

*[Puedes mostrar en pantalla el código de la declaración]*

```csharp
// Cola para la fila de pasajeros (FIFO)
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();
```

---

## ?? PARTE 2: ESTRUCTURA DEL CÓDIGO (3-4 minutos)

### Clases Principales

Mi sistema tiene 3 clases principales:

#### 1. **Clase Pasajero** (Modelo de datos)

Esta clase representa a cada pasajero que se anota. Tiene:
- **Nombre:** El nombre del pasajero
- **Tipo:** Si es Normal, Estudiante o Jubilado
- **HoraAnotacion:** Cuándo se anotó (para calcular tiempo de espera)
- **Tarifa:** Una propiedad calculada que devuelve el precio según el tipo

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

Lo interesante acá es que la tarifa se **calcula automáticamente**. No la guardo, la calculo cada vez que la necesito.

#### 2. **Clase EstadisticasDiarias** (Estadísticas)

Esta clase lleva el registro de:
- Total de viajes del día
- Total de pasajeros transportados
- Recaudación total
- Desglose por tipo de pasajero

También tiene una lista interna de todos los viajes para poder generar el reporte al final del día.

#### 3. **Form1** (Lógica principal)

Esta es la clase más grande. Acá está toda la lógica de:
- Anotar pasajeros
- Iniciar viajes
- Manejar el temporizador
- Guardar y cargar datos

---

## ?? PARTE 3: FUNCIONALIDADES IMPLEMENTADAS (4-5 minutos)

### 1. Gestión de Pasajeros

Cuando el usuario clickea "Anotar":
1. **Valido** que haya escrito un nombre
2. **Verifico** que la combi no esté llena (19 lugares máximo)
3. **Creo** el objeto Pasajero con el tipo seleccionado
4. Lo **agrego a la cola** con `Enqueue()`
5. **Actualizo** la lista en pantalla

*[Si puedes, muestra en pantalla la función btnAnotar_Click]*

### 2. Temporizador Automático (20 minutos)

Esta fue una de las partes más interesantes. Cuando se anota el primer pasajero:
- Se activa un temporizador de 20 minutos
- Cada segundo se actualiza el contador en pantalla
- El color cambia según el tiempo restante (azul ? naranja ? rojo)
- Si llega a 00:00, **la combi parte automáticamente**

```csharp
private void timerCombi_Tick(object sender, EventArgs e)
{
    tiempoRestanteSegundos--;
    ActualizarTiempoRestante();
    
    if (tiempoRestanteSegundos <= 0)
    {
        // La combi sale sola
        btnSubir_Click(sender, e);
    }
}
```

### 3. Tres Rutas Diferentes

Implementé 3 rutas que el usuario puede elegir:
- **Ruta 1:** Obelisco ? Puerto Madero (20 min)
- **Ruta 2:** Obelisco ? Recoleta (25 min)
- **Ruta 3:** Obelisco ? Palermo (30 min)

Cuando inicia un viaje, se abre un formulario modal con RadioButtons para elegir la ruta.

### 4. Estadísticas en Tiempo Real

El sistema muestra en tiempo real:
- Cantidad de viajes realizados
- Total de pasajeros transportados
- Recaudación total del día

Esto se actualiza automáticamente después de cada viaje.

### 5. Persistencia de Datos

Implementé persistencia usando archivos de texto:

**fila_espera.txt** - Guarda los pasajeros en espera
```
Juan Perez|0|2025-01-13 08:15:30
Ana Garcia|1|2025-01-13 08:17:45
```

**estadisticas.txt** - Guarda las estadísticas del día
```
Fecha|2025-01-13
TotalViajes|5
TotalPasajeros|73
RecaudacionTotal|28250.00
```

Lo interesante es que cuando cierras y volvés a abrir:
- Se cargan todos los pasajeros que estaban esperando
- El temporizador **continúa desde donde quedó** (calcula el tiempo transcurrido)

```csharp
var primerPasajero = filaDeEspera.Peek();
int tiempoTranscurrido = (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
tiempoRestanteSegundos = Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);
```

### 6. Generación de Reportes

Al final del día se puede generar un reporte completo en formato .txt que incluye:
- Resumen general (viajes, pasajeros, recaudación)
- Desglose por tipo de pasajero con porcentajes
- Recaudación por tipo
- Detalle de cada viaje
- Promedios calculados automáticamente

El archivo se abre automáticamente en Notepad.

---

## ?? PARTE 4: DECISIONES TÉCNICAS (2-3 minutos)

### Estructuras Dinámicas

Usé estructuras dinámicas porque:
- **Queue<Pasajero>**: Crece automáticamente según los pasajeros que se anotan
- **List<Pasajero>**: Para almacenar temporalmente los pasajeros en viaje
- **List<Viaje>**: Para el historial de viajes del día

No tengo arrays de tamaño fijo, todo es dinámico.

### Validaciones Implementadas

Implementé validaciones robustas:
1. **Campo vacío:** No dejo anotar sin nombre
2. **Capacidad máxima:** No dejo anotar más de 19 pasajeros
3. **Mínimo de pasajeros:** Verifico que haya al menos 1 para partir
4. **Confirmación de cierre:** Si hay pasajeros esperando, pregunto antes de cerrar

### Uso de LINQ

Usé LINQ para hacer consultas sobre las listas:

```csharp
// Contar pasajeros normales
int normales = pasajerosEnCombi.Count(p => p.Tipo == Pasajero.TipoPasajero.Normal);

// Calcular recaudación total
decimal recaudacion = pasajerosEnCombi.Sum(p => p.Tarifa);
```

---

## ?? PARTE 5: INTERFAZ DE USUARIO (1-2 minutos)

Diseñé la interfaz pensando en la usabilidad:

### Colores Significativos
- **Azul:** Para elementos normales
- **Verde:** Para el botón de iniciar viaje (acción positiva)
- **Naranja:** Para el botón de reporte (información)
- **Rojo:** Para cerrar y para el temporizador urgente

### Controles Responsive
Usé la propiedad `Anchor` para que la ventana se pueda redimensionar y los controles se ajusten automáticamente.

### Símbolos ASCII
En lugar de emojis (que pueden dar problemas), usé símbolos ASCII:
- `[N]` para Normal
- `[E]` para Estudiante
- `[J]` para Jubilado

Esto garantiza compatibilidad en cualquier sistema.

---

## ?? PARTE 6: PRUEBAS REALIZADAS (2 minutos)

Probé exhaustivamente el sistema con diferentes casos:

### Caso 1: Flujo Normal
- Anotar 3 pasajeros de diferentes tipos
- Iniciar viaje manualmente
- Verificar que la recaudación sea correcta

**Resultado:** ? Funciona correctamente
- Normal $500 + Estudiante $250 + Jubilado $0 = $750

### Caso 2: Temporizador Automático
- Anotar 1 pasajero
- Esperar 20 minutos (lo probé con 10 segundos para testing)
- Verificar que la combi parta sola

**Resultado:** ? Funciona correctamente

### Caso 3: Persistencia
- Anotar pasajeros
- Cerrar la aplicación
- Reabrir
- Verificar que los pasajeros estén ahí
- Verificar que el temporizador continúe

**Resultado:** ? Funciona correctamente

### Caso 4: Capacidad Máxima
- Anotar 19 pasajeros
- Intentar anotar el #20

**Resultado:** ? Muestra mensaje "Combi llena"

### Caso 5: Reportes
- Realizar varios viajes
- Generar reporte
- Verificar cálculos

**Resultado:** ? Todos los cálculos son correctos

---

## ?? PARTE 7: DESAFÍOS Y SOLUCIONES (2 minutos)

### Desafío 1: Recorrer Queue sin modificarla

**Problema:** Necesitaba mostrar todos los pasajeros en el ListBox pero `foreach` directamente en Queue los saca.

**Solución:** Usar `ToArray()` para crear una copia temporal:
```csharp
foreach (Pasajero pasajero in filaDeEspera.ToArray())
{
    lstEnEspera.Items.Add($"{posicion}. {pasajero}");
}
```

### Desafío 2: Persistencia del Temporizador

**Problema:** Al cerrar y reabrir, el temporizador no consideraba el tiempo que pasó mientras estaba cerrado.

**Solución:** Calcular la diferencia entre `DateTime.Now` y `HoraAnotacion` del primer pasajero:
```csharp
int tiempoTranscurrido = (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
tiempoRestanteSegundos = Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);
```

### Desafío 3: Formato del Reporte

**Problema:** Generar un reporte legible y bien formateado.

**Solución:** Usar `StringBuilder` y construir el texto línea por línea con encabezados y separadores.

---

## ?? PARTE 8: CONOCIMIENTOS APLICADOS (1-2 minutos)

En este TP apliqué conceptos de:

### De la Cursada:
- ? **Colas (Queue)** - FIFO
- ? **Listas (List)** - Estructuras dinámicas
- ? **Archivos** - StreamWriter/StreamReader
- ? **Validaciones** - Control de datos de entrada

### Adicionales:
- ? **POO** - Clases, propiedades, métodos
- ? **Enumeraciones** - TipoPasajero
- ? **LINQ** - Count(), Sum(), Where()
- ? **Eventos** - Click, Tick, FormClosing
- ? **Temporizadores** - Timer control
- ? **Serialización** - Formato CSV personalizado

---

## ?? PARTE 9: CONCLUSIONES (1-2 minutos)

### Objetivos Cumplidos

? Implementé correctamente una Cola para la fila de espera  
? Apliqué el principio FIFO de forma efectiva  
? Desarrollé una interfaz intuitiva y funcional  
? Implementé validaciones robustas  
? Agregué funcionalidades extra (temporizador, estadísticas, reportes)  
? Documenté el código de forma clara  
? El sistema es escalable y fácil de mantener

### Aprendizajes Principales

1. **Estructuras de datos:** Ahora entiendo perfectamente cuándo usar Queue vs Stack vs List
2. **Persistencia:** Aprendí a guardar y cargar datos de forma eficiente
3. **Eventos:** Mejoré mi manejo de eventos en Windows Forms
4. **LINQ:** Descubrí lo útil que es para consultas sobre colecciones
5. **Buenas prácticas:** Validaciones, manejo de errores, código limpio

### Posibles Mejoras Futuras

Si tuviera más tiempo, agregaría:
- Base de datos SQL en lugar de archivos de texto
- Múltiples terminales funcionando simultáneamente
- Sistema de reservas anticipadas
- Gráficos de estadísticas
- Exportación de reportes a PDF

---

## ?? PARTE 10: PREGUNTAS FRECUENTES (Preparación)

### Pregunta: ¿Por qué Queue y no otra estructura?

**Respuesta:** Porque una fila de espera en la vida real es FIFO. El primero que llega es el primero que se atiende. Si hubiera usado Stack (LIFO), el último en llegar sería el primero en subir, lo cual no tiene sentido en este contexto. Una List tampoco sería apropiada porque requeriría manejar manualmente los índices para simular FIFO.

### Pregunta: ¿Cómo manejaste la persistencia?

**Respuesta:** Usé dos archivos de texto en formato CSV:
- `fila_espera.txt`: Guarda los pasajeros en espera con su tipo y hora
- `estadisticas.txt`: Guarda las estadísticas del día

Al cerrar guardo todo, y al abrir verifico si los archivos existen y cargo los datos. Para las estadísticas, además verifico que sean del día actual, si no, empiezo de cero.

### Pregunta: ¿Qué pasa si se corta la luz mientras hay pasajeros?

**Respuesta:** Los datos se guardan solo al cerrar la aplicación normalmente. Si se corta la luz, se perderían los pasajeros que estaban en espera en ese momento. Una mejora sería implementar auto-guardado cada cierto tiempo, pero no lo incluí en esta versión para mantener el código simple.

### Pregunta: ¿Cómo calculaste las tarifas?

**Respuesta:** Usé una propiedad calculada en la clase Pasajero. No guardo la tarifa, la calculo on-demand usando un `switch expression` que devuelve el valor según el tipo de pasajero. Esto es más eficiente y evita inconsistencias.

### Pregunta: ¿Por qué no usaste una base de datos?

**Respuesta:** El enunciado del TP no lo requería, y para un sistema pequeño como este, archivos de texto son suficientes. Son más simples de implementar y debuggear. Para un sistema en producción, definitivamente usaría SQL Server o SQLite.

---

## ?? DEMOSTRACIÓN EN VIVO (3-5 minutos)

*[Si te piden demostrar el sistema]*

### Paso 1: Mostrar Pantalla Inicial
"Acá ven la pantalla principal. Arriba está el título, a la izquierda el panel donde anoto pasajeros, y a la derecha las estadísticas del día."

### Paso 2: Anotar Pasajeros
"Voy a anotar 3 pasajeros de diferentes tipos..."
- Juan Pérez - Normal
- María García - Estudiante
- Luis López - Jubilado

"Ven que cuando anoto el primero, arranca el temporizador en 20:00."

### Paso 3: Mostrar Temporizador
"El temporizador va bajando cada segundo. Si espero 20 minutos, la combi sale sola automáticamente."

### Paso 4: Iniciar Viaje
"Ahora inicio el viaje manualmente. Me pide que elija la ruta..."
*[Seleccionar ruta]*

"Y acá me muestra todos los detalles: los 3 pasajeros, el desglose por tipo, y la recaudación total de $750."

### Paso 5: Mostrar Estadísticas
"Ven que las estadísticas se actualizaron: 1 viaje, 3 pasajeros, $750 de recaudación."

### Paso 6: Generar Reporte
"Ahora genero el reporte del día..."
*[Click en Generar Reporte]*

"Se abre automáticamente en Notepad con toda la información formateada."

### Paso 7: Mostrar Persistencia
"Si cierro la aplicación y la vuelvo a abrir..."
*[Anotar 2 pasajeros, cerrar, reabrir]*

"Ven que los pasajeros siguen ahí y el temporizador continúa desde donde quedó."

---

## ?? CIERRE (30 segundos)

Para finalizar, este proyecto me permitió aplicar de forma práctica todo lo que aprendimos sobre estructuras de datos. Entendí realmente la diferencia entre Queue, Stack y List, no solo en teoría sino implementándolas en un caso de uso real.

El código está subido en GitHub en el repositorio https://github.com/ignaciomondragon24/tp12 y toda la documentación está disponible.

Muchas gracias por su atención. Quedo a disposición para cualquier pregunta.

---

## ?? NOTAS PARA LA DEFENSA

### Tips para el día de la defensa:

1. **Practica antes** - Lee este speech varias veces
2. **No memorices textualmente** - Entiende los conceptos y hablá con tus palabras
3. **Lleva la aplicación funcionando** - Por si te piden mostrarla
4. **Tené el código abierto** - En Visual Studio
5. **Relájate** - Vos hiciste el trabajo, lo conocés

### Si te bloqueas:

- Respirá hondo
- Mirá las secciones de este documento
- Explicá con tus palabras
- Mostrá el código en pantalla si es más fácil

### Timing Sugerido:

| Sección | Tiempo |
|---------|--------|
| Introducción | 1-2 min |
| Análisis del Problema | 2-3 min |
| Estructura del Código | 3-4 min |
| Funcionalidades | 4-5 min |
| Decisiones Técnicas | 2-3 min |
| Interfaz | 1-2 min |
| Pruebas | 2 min |
| Desafíos | 2 min |
| Conocimientos | 1-2 min |
| Conclusiones | 1-2 min |
| **TOTAL** | **20-30 min** |

### Frases Clave para Usar:

- "Elegí Queue porque necesitaba FIFO"
- "Implementé validaciones robustas"
- "Usé estructuras dinámicas que crecen automáticamente"
- "La persistencia restaura el estado completo"
- "Apliqué LINQ para consultas eficientes"
- "El código está bien documentado y es mantenible"

---

## ? CHECKLIST PRE-DEFENSA

Antes de la defensa, verificá que:

- [ ] La aplicación compila sin errores
- [ ] Todos los casos de uso funcionan
- [ ] Los archivos de persistencia se crean correctamente
- [ ] El reporte se genera bien
- [ ] Tenés el código abierto en Visual Studio
- [ ] Tenés este documento a mano
- [ ] Practicaste al menos una vez
- [ ] Sabés responder las preguntas frecuentes
- [ ] Estás listo para mostrar el sistema

---

**¡MUCHA SUERTE EN TU DEFENSA! ??**

Recordá: Vos hiciste este trabajo, lo entendés, y lo vas a defender muy bien.
