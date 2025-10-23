# DEFENSA ORAL - TP 12 INTEGRACI�N
## Sistema de Gesti�n de Combis - Terminal Obelisco

**Alumno:** [Tu Nombre]  
**Legajo:** [Tu Legajo]  
**Materia:** Programaci�n y Estructuras de Datos  
**Universidad:** UAI - Universidad Abierta Interamericana

---

## ?? INTRODUCCI�N (1-2 minutos)

Buenas tardes, profesor/a. Hoy voy a presentar mi Trabajo Pr�ctico N�12 que consiste en un sistema de gesti�n para un servicio de combis.

El sistema maneja la fila de espera de pasajeros en la Terminal Obelisco y permite gestionar viajes hacia diferentes destinos de Buenos Aires. Lo desarroll� en C# usando Windows Forms y .NET 8.

El objetivo principal era aplicar las estructuras de datos que vimos en la cursada, espec�ficamente **colas (Queue)**, para simular una situaci�n real: una fila de personas esperando para subir a una combi.

---

## ?? PARTE 1: AN�LISIS DEL PROBLEMA (2-3 minutos)

### �Por qu� eleg� una Cola (Queue)?

Esta fue mi primera decisi�n importante. Analic� el problema y me di cuenta de que una fila de espera en la vida real funciona con el principio **FIFO - First In, First Out**.

**�Qu� significa esto?**  
Que el primer pasajero que llega es el primero que sube a la combi. Esto es justo y es exactamente c�mo funciona una Cola.

**�Por qu� NO us� una Pila (Stack)?**  
Una Pila funciona con LIFO - Last In, First Out. Esto significa que el �ltimo en llegar ser�a el primero en subir, lo cual ser�a totalmente injusto para los que esperaron m�s tiempo.

*[Puedes mostrar en pantalla el c�digo de la declaraci�n]*

```csharp
// Cola para la fila de pasajeros (FIFO)
private Queue<Pasajero> filaDeEspera = new Queue<Pasajero>();
```

---

## ?? PARTE 2: ESTRUCTURA DEL C�DIGO (3-4 minutos)

### Clases Principales

Mi sistema tiene 3 clases principales:

#### 1. **Clase Pasajero** (Modelo de datos)

Esta clase representa a cada pasajero que se anota. Tiene:
- **Nombre:** El nombre del pasajero
- **Tipo:** Si es Normal, Estudiante o Jubilado
- **HoraAnotacion:** Cu�ndo se anot� (para calcular tiempo de espera)
- **Tarifa:** Una propiedad calculada que devuelve el precio seg�n el tipo

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

Lo interesante ac� es que la tarifa se **calcula autom�ticamente**. No la guardo, la calculo cada vez que la necesito.

#### 2. **Clase EstadisticasDiarias** (Estad�sticas)

Esta clase lleva el registro de:
- Total de viajes del d�a
- Total de pasajeros transportados
- Recaudaci�n total
- Desglose por tipo de pasajero

Tambi�n tiene una lista interna de todos los viajes para poder generar el reporte al final del d�a.

#### 3. **Form1** (L�gica principal)

Esta es la clase m�s grande. Ac� est� toda la l�gica de:
- Anotar pasajeros
- Iniciar viajes
- Manejar el temporizador
- Guardar y cargar datos

---

## ?? PARTE 3: FUNCIONALIDADES IMPLEMENTADAS (4-5 minutos)

### 1. Gesti�n de Pasajeros

Cuando el usuario clickea "Anotar":
1. **Valido** que haya escrito un nombre
2. **Verifico** que la combi no est� llena (19 lugares m�ximo)
3. **Creo** el objeto Pasajero con el tipo seleccionado
4. Lo **agrego a la cola** con `Enqueue()`
5. **Actualizo** la lista en pantalla

*[Si puedes, muestra en pantalla la funci�n btnAnotar_Click]*

### 2. Temporizador Autom�tico (20 minutos)

Esta fue una de las partes m�s interesantes. Cuando se anota el primer pasajero:
- Se activa un temporizador de 20 minutos
- Cada segundo se actualiza el contador en pantalla
- El color cambia seg�n el tiempo restante (azul ? naranja ? rojo)
- Si llega a 00:00, **la combi parte autom�ticamente**

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

Implement� 3 rutas que el usuario puede elegir:
- **Ruta 1:** Obelisco ? Puerto Madero (20 min)
- **Ruta 2:** Obelisco ? Recoleta (25 min)
- **Ruta 3:** Obelisco ? Palermo (30 min)

Cuando inicia un viaje, se abre un formulario modal con RadioButtons para elegir la ruta.

### 4. Estad�sticas en Tiempo Real

El sistema muestra en tiempo real:
- Cantidad de viajes realizados
- Total de pasajeros transportados
- Recaudaci�n total del d�a

Esto se actualiza autom�ticamente despu�s de cada viaje.

### 5. Persistencia de Datos

Implement� persistencia usando archivos de texto:

**fila_espera.txt** - Guarda los pasajeros en espera
```
Juan Perez|0|2025-01-13 08:15:30
Ana Garcia|1|2025-01-13 08:17:45
```

**estadisticas.txt** - Guarda las estad�sticas del d�a
```
Fecha|2025-01-13
TotalViajes|5
TotalPasajeros|73
RecaudacionTotal|28250.00
```

Lo interesante es que cuando cierras y volv�s a abrir:
- Se cargan todos los pasajeros que estaban esperando
- El temporizador **contin�a desde donde qued�** (calcula el tiempo transcurrido)

```csharp
var primerPasajero = filaDeEspera.Peek();
int tiempoTranscurrido = (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
tiempoRestanteSegundos = Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);
```

### 6. Generaci�n de Reportes

Al final del d�a se puede generar un reporte completo en formato .txt que incluye:
- Resumen general (viajes, pasajeros, recaudaci�n)
- Desglose por tipo de pasajero con porcentajes
- Recaudaci�n por tipo
- Detalle de cada viaje
- Promedios calculados autom�ticamente

El archivo se abre autom�ticamente en Notepad.

---

## ?? PARTE 4: DECISIONES T�CNICAS (2-3 minutos)

### Estructuras Din�micas

Us� estructuras din�micas porque:
- **Queue<Pasajero>**: Crece autom�ticamente seg�n los pasajeros que se anotan
- **List<Pasajero>**: Para almacenar temporalmente los pasajeros en viaje
- **List<Viaje>**: Para el historial de viajes del d�a

No tengo arrays de tama�o fijo, todo es din�mico.

### Validaciones Implementadas

Implement� validaciones robustas:
1. **Campo vac�o:** No dejo anotar sin nombre
2. **Capacidad m�xima:** No dejo anotar m�s de 19 pasajeros
3. **M�nimo de pasajeros:** Verifico que haya al menos 1 para partir
4. **Confirmaci�n de cierre:** Si hay pasajeros esperando, pregunto antes de cerrar

### Uso de LINQ

Us� LINQ para hacer consultas sobre las listas:

```csharp
// Contar pasajeros normales
int normales = pasajerosEnCombi.Count(p => p.Tipo == Pasajero.TipoPasajero.Normal);

// Calcular recaudaci�n total
decimal recaudacion = pasajerosEnCombi.Sum(p => p.Tarifa);
```

---

## ?? PARTE 5: INTERFAZ DE USUARIO (1-2 minutos)

Dise�� la interfaz pensando en la usabilidad:

### Colores Significativos
- **Azul:** Para elementos normales
- **Verde:** Para el bot�n de iniciar viaje (acci�n positiva)
- **Naranja:** Para el bot�n de reporte (informaci�n)
- **Rojo:** Para cerrar y para el temporizador urgente

### Controles Responsive
Us� la propiedad `Anchor` para que la ventana se pueda redimensionar y los controles se ajusten autom�ticamente.

### S�mbolos ASCII
En lugar de emojis (que pueden dar problemas), us� s�mbolos ASCII:
- `[N]` para Normal
- `[E]` para Estudiante
- `[J]` para Jubilado

Esto garantiza compatibilidad en cualquier sistema.

---

## ?? PARTE 6: PRUEBAS REALIZADAS (2 minutos)

Prob� exhaustivamente el sistema con diferentes casos:

### Caso 1: Flujo Normal
- Anotar 3 pasajeros de diferentes tipos
- Iniciar viaje manualmente
- Verificar que la recaudaci�n sea correcta

**Resultado:** ? Funciona correctamente
- Normal $500 + Estudiante $250 + Jubilado $0 = $750

### Caso 2: Temporizador Autom�tico
- Anotar 1 pasajero
- Esperar 20 minutos (lo prob� con 10 segundos para testing)
- Verificar que la combi parta sola

**Resultado:** ? Funciona correctamente

### Caso 3: Persistencia
- Anotar pasajeros
- Cerrar la aplicaci�n
- Reabrir
- Verificar que los pasajeros est�n ah�
- Verificar que el temporizador contin�e

**Resultado:** ? Funciona correctamente

### Caso 4: Capacidad M�xima
- Anotar 19 pasajeros
- Intentar anotar el #20

**Resultado:** ? Muestra mensaje "Combi llena"

### Caso 5: Reportes
- Realizar varios viajes
- Generar reporte
- Verificar c�lculos

**Resultado:** ? Todos los c�lculos son correctos

---

## ?? PARTE 7: DESAF�OS Y SOLUCIONES (2 minutos)

### Desaf�o 1: Recorrer Queue sin modificarla

**Problema:** Necesitaba mostrar todos los pasajeros en el ListBox pero `foreach` directamente en Queue los saca.

**Soluci�n:** Usar `ToArray()` para crear una copia temporal:
```csharp
foreach (Pasajero pasajero in filaDeEspera.ToArray())
{
    lstEnEspera.Items.Add($"{posicion}. {pasajero}");
}
```

### Desaf�o 2: Persistencia del Temporizador

**Problema:** Al cerrar y reabrir, el temporizador no consideraba el tiempo que pas� mientras estaba cerrado.

**Soluci�n:** Calcular la diferencia entre `DateTime.Now` y `HoraAnotacion` del primer pasajero:
```csharp
int tiempoTranscurrido = (int)(DateTime.Now - primerPasajero.HoraAnotacion).TotalSeconds;
tiempoRestanteSegundos = Math.Max(0, TIEMPO_ESPERA_SEGUNDOS - tiempoTranscurrido);
```

### Desaf�o 3: Formato del Reporte

**Problema:** Generar un reporte legible y bien formateado.

**Soluci�n:** Usar `StringBuilder` y construir el texto l�nea por l�nea con encabezados y separadores.

---

## ?? PARTE 8: CONOCIMIENTOS APLICADOS (1-2 minutos)

En este TP apliqu� conceptos de:

### De la Cursada:
- ? **Colas (Queue)** - FIFO
- ? **Listas (List)** - Estructuras din�micas
- ? **Archivos** - StreamWriter/StreamReader
- ? **Validaciones** - Control de datos de entrada

### Adicionales:
- ? **POO** - Clases, propiedades, m�todos
- ? **Enumeraciones** - TipoPasajero
- ? **LINQ** - Count(), Sum(), Where()
- ? **Eventos** - Click, Tick, FormClosing
- ? **Temporizadores** - Timer control
- ? **Serializaci�n** - Formato CSV personalizado

---

## ?? PARTE 9: CONCLUSIONES (1-2 minutos)

### Objetivos Cumplidos

? Implement� correctamente una Cola para la fila de espera  
? Apliqu� el principio FIFO de forma efectiva  
? Desarroll� una interfaz intuitiva y funcional  
? Implement� validaciones robustas  
? Agregu� funcionalidades extra (temporizador, estad�sticas, reportes)  
? Document� el c�digo de forma clara  
? El sistema es escalable y f�cil de mantener

### Aprendizajes Principales

1. **Estructuras de datos:** Ahora entiendo perfectamente cu�ndo usar Queue vs Stack vs List
2. **Persistencia:** Aprend� a guardar y cargar datos de forma eficiente
3. **Eventos:** Mejor� mi manejo de eventos en Windows Forms
4. **LINQ:** Descubr� lo �til que es para consultas sobre colecciones
5. **Buenas pr�cticas:** Validaciones, manejo de errores, c�digo limpio

### Posibles Mejoras Futuras

Si tuviera m�s tiempo, agregar�a:
- Base de datos SQL en lugar de archivos de texto
- M�ltiples terminales funcionando simult�neamente
- Sistema de reservas anticipadas
- Gr�ficos de estad�sticas
- Exportaci�n de reportes a PDF

---

## ?? PARTE 10: PREGUNTAS FRECUENTES (Preparaci�n)

### Pregunta: �Por qu� Queue y no otra estructura?

**Respuesta:** Porque una fila de espera en la vida real es FIFO. El primero que llega es el primero que se atiende. Si hubiera usado Stack (LIFO), el �ltimo en llegar ser�a el primero en subir, lo cual no tiene sentido en este contexto. Una List tampoco ser�a apropiada porque requerir�a manejar manualmente los �ndices para simular FIFO.

### Pregunta: �C�mo manejaste la persistencia?

**Respuesta:** Us� dos archivos de texto en formato CSV:
- `fila_espera.txt`: Guarda los pasajeros en espera con su tipo y hora
- `estadisticas.txt`: Guarda las estad�sticas del d�a

Al cerrar guardo todo, y al abrir verifico si los archivos existen y cargo los datos. Para las estad�sticas, adem�s verifico que sean del d�a actual, si no, empiezo de cero.

### Pregunta: �Qu� pasa si se corta la luz mientras hay pasajeros?

**Respuesta:** Los datos se guardan solo al cerrar la aplicaci�n normalmente. Si se corta la luz, se perder�an los pasajeros que estaban en espera en ese momento. Una mejora ser�a implementar auto-guardado cada cierto tiempo, pero no lo inclu� en esta versi�n para mantener el c�digo simple.

### Pregunta: �C�mo calculaste las tarifas?

**Respuesta:** Us� una propiedad calculada en la clase Pasajero. No guardo la tarifa, la calculo on-demand usando un `switch expression` que devuelve el valor seg�n el tipo de pasajero. Esto es m�s eficiente y evita inconsistencias.

### Pregunta: �Por qu� no usaste una base de datos?

**Respuesta:** El enunciado del TP no lo requer�a, y para un sistema peque�o como este, archivos de texto son suficientes. Son m�s simples de implementar y debuggear. Para un sistema en producci�n, definitivamente usar�a SQL Server o SQLite.

---

## ?? DEMOSTRACI�N EN VIVO (3-5 minutos)

*[Si te piden demostrar el sistema]*

### Paso 1: Mostrar Pantalla Inicial
"Ac� ven la pantalla principal. Arriba est� el t�tulo, a la izquierda el panel donde anoto pasajeros, y a la derecha las estad�sticas del d�a."

### Paso 2: Anotar Pasajeros
"Voy a anotar 3 pasajeros de diferentes tipos..."
- Juan P�rez - Normal
- Mar�a Garc�a - Estudiante
- Luis L�pez - Jubilado

"Ven que cuando anoto el primero, arranca el temporizador en 20:00."

### Paso 3: Mostrar Temporizador
"El temporizador va bajando cada segundo. Si espero 20 minutos, la combi sale sola autom�ticamente."

### Paso 4: Iniciar Viaje
"Ahora inicio el viaje manualmente. Me pide que elija la ruta..."
*[Seleccionar ruta]*

"Y ac� me muestra todos los detalles: los 3 pasajeros, el desglose por tipo, y la recaudaci�n total de $750."

### Paso 5: Mostrar Estad�sticas
"Ven que las estad�sticas se actualizaron: 1 viaje, 3 pasajeros, $750 de recaudaci�n."

### Paso 6: Generar Reporte
"Ahora genero el reporte del d�a..."
*[Click en Generar Reporte]*

"Se abre autom�ticamente en Notepad con toda la informaci�n formateada."

### Paso 7: Mostrar Persistencia
"Si cierro la aplicaci�n y la vuelvo a abrir..."
*[Anotar 2 pasajeros, cerrar, reabrir]*

"Ven que los pasajeros siguen ah� y el temporizador contin�a desde donde qued�."

---

## ?? CIERRE (30 segundos)

Para finalizar, este proyecto me permiti� aplicar de forma pr�ctica todo lo que aprendimos sobre estructuras de datos. Entend� realmente la diferencia entre Queue, Stack y List, no solo en teor�a sino implement�ndolas en un caso de uso real.

El c�digo est� subido en GitHub en el repositorio https://github.com/ignaciomondragon24/tp12 y toda la documentaci�n est� disponible.

Muchas gracias por su atenci�n. Quedo a disposici�n para cualquier pregunta.

---

## ?? NOTAS PARA LA DEFENSA

### Tips para el d�a de la defensa:

1. **Practica antes** - Lee este speech varias veces
2. **No memorices textualmente** - Entiende los conceptos y habl� con tus palabras
3. **Lleva la aplicaci�n funcionando** - Por si te piden mostrarla
4. **Ten� el c�digo abierto** - En Visual Studio
5. **Rel�jate** - Vos hiciste el trabajo, lo conoc�s

### Si te bloqueas:

- Respir� hondo
- Mir� las secciones de este documento
- Explic� con tus palabras
- Mostr� el c�digo en pantalla si es m�s f�cil

### Timing Sugerido:

| Secci�n | Tiempo |
|---------|--------|
| Introducci�n | 1-2 min |
| An�lisis del Problema | 2-3 min |
| Estructura del C�digo | 3-4 min |
| Funcionalidades | 4-5 min |
| Decisiones T�cnicas | 2-3 min |
| Interfaz | 1-2 min |
| Pruebas | 2 min |
| Desaf�os | 2 min |
| Conocimientos | 1-2 min |
| Conclusiones | 1-2 min |
| **TOTAL** | **20-30 min** |

### Frases Clave para Usar:

- "Eleg� Queue porque necesitaba FIFO"
- "Implement� validaciones robustas"
- "Us� estructuras din�micas que crecen autom�ticamente"
- "La persistencia restaura el estado completo"
- "Apliqu� LINQ para consultas eficientes"
- "El c�digo est� bien documentado y es mantenible"

---

## ? CHECKLIST PRE-DEFENSA

Antes de la defensa, verific� que:

- [ ] La aplicaci�n compila sin errores
- [ ] Todos los casos de uso funcionan
- [ ] Los archivos de persistencia se crean correctamente
- [ ] El reporte se genera bien
- [ ] Ten�s el c�digo abierto en Visual Studio
- [ ] Ten�s este documento a mano
- [ ] Practicaste al menos una vez
- [ ] Sab�s responder las preguntas frecuentes
- [ ] Est�s listo para mostrar el sistema

---

**�MUCHA SUERTE EN TU DEFENSA! ??**

Record�: Vos hiciste este trabajo, lo entend�s, y lo vas a defender muy bien.
