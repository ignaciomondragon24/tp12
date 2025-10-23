# ?? GU�A R�PIDA DE INICIO
## Sistema de Combis - Versi�n Mejorada

---

## ? INICIO R�PIDO (5 MINUTOS)

### 1?? **Abrir el Proyecto**
```
1. Visual Studio 2022
2. Archivo ? Abrir ? Proyecto/Soluci�n
3. Seleccionar: AppCombis.sln
4. Presionar F5 (o click en ? Iniciar)
```

---

### 2?? **Primera Prueba: Viaje Simple**

#### Paso 1: Anotar Pasajeros
```
1. Escribir "Juan P�rez"
2. Seleccionar "?? Normal - $500"
3. Click "Anotar"
   ? Temporizador inicia: 20:00

4. Escribir "Mar�a Garc�a"
5. Seleccionar "?? Estudiante - $250"
6. Click "Anotar"

7. Escribir "Roberto L�pez"
8. Seleccionar "?? Jubilado - Gratis"
9. Click "Anotar"
```

#### Paso 2: Iniciar Viaje
```
1. Click "?? Subir a la combi (Iniciar Viaje)"
2. Click "Yes" para confirmar
3. Seleccionar "?? Ruta 1: Obelisco ? Puerto Madero"
4. Click "Confirmar"
5. Ver detalles del viaje:
   - 3 pasajeros
   - Recaudaci�n: $750
   - Desglose por tipo
```

#### Paso 3: Ver Estad�sticas
```
Observar panel "?? Estad�sticas":
?? Viajes: 1
?? Pasajeros: 3
?? Recaudaci�n: $750.00
```

---

### 3?? **Generar Reporte**
```
1. Click "?? Generar Reporte Del D�a"
2. Click "Yes" para abrir en Notepad
3. Ver reporte completo con estad�sticas
```

---

## ?? FUNCIONALIDADES PRINCIPALES

### ?? **Tipos de Pasajeros**
| Tipo | Tarifa | C�mo Seleccionar |
|------|--------|------------------|
| ?? Normal | $500 | ComboBox ? "?? Normal - $500" |
| ?? Estudiante | $250 | ComboBox ? "?? Estudiante - $250" |
| ?? Jubilado | $0 | ComboBox ? "?? Jubilado - Gratis" |

### ? **Temporizador**
- **Inicia:** Al anotar el primer pasajero
- **Duraci�n:** 20:00 minutos
- **Colores:**
  - ?? Azul: +5 minutos
  - ?? Naranja: 1-5 minutos
  - ?? Rojo: <1 minuto
- **Salida autom�tica:** Cuando llega a 00:00

### ??? **Rutas Disponibles**
- **?? Ruta 1:** Obelisco ? Puerto Madero (20 min)
- **?? Ruta 2:** Obelisco ? Recoleta (25 min)
- **?? Ruta 3:** Obelisco ? Palermo (30 min)

---

## ?? PRUEBAS R�PIDAS

### ? Prueba 1: Diferentes Tipos de Pasajeros
```
1. Anotar 5 Normal, 3 Estudiante, 2 Jubilado
2. Iniciar viaje
3. Verificar recaudaci�n:
   (5 � $500) + (3 � $250) + (2 � $0) = $3,250
```

### ? Prueba 2: Temporizador Autom�tico
```
1. Anotar 1 pasajero
2. NO presionar "Subir"
3. Esperar 20 minutos (o modificar const a 10 segundos para prueba)
4. Combi parte autom�ticamente
```

### ? Prueba 3: Persistencia
```
1. Anotar 3 pasajeros
2. Cerrar aplicaci�n
3. Reabrir
4. Pasajeros est�n ah�
5. Temporizador contin�a desde donde qued�
```

### ? Prueba 4: Capacidad M�xima
```
1. Anotar 19 pasajeros
2. Intentar anotar el #20
3. Mensaje: "Combi llena"
```

### ? Prueba 5: Reporte Completo
```
1. Realizar 3 viajes diferentes
2. Click "Generar Reporte"
3. Ver archivo con detalles de los 3 viajes
```

---

## ?? SOLUCI�N DE PROBLEMAS

### ? Error: "No se puede iniciar el temporizador"
**Soluci�n:** Verificar que `timerCombi.Interval = 1000` en el Designer

### ? Error: "No se encuentra el archivo"
**Soluci�n:** Los archivos se crean en `bin\Debug\net8.0-windows\`

### ? Error: "La combi no parte autom�ticamente"
**Soluci�n:** Verificar que el evento `timerCombi_Tick` est� conectado

### ? El temporizador no cambia de color
**Soluci�n:** Verificar la l�gica en `ActualizarTiempoRestante()`

---

## ?? VALORES DE EJEMPLO PARA PRUEBAS

### Conjunto 1: Balanceado
```
5 Normales
5 Estudiantes
5 Jubilados
Total: 15 pasajeros
Recaudaci�n esperada: $3,750
```

### Conjunto 2: Mayor�a Normal
```
10 Normales
3 Estudiantes
2 Jubilados
Total: 15 pasajeros
Recaudaci�n esperada: $5,750
```

### Conjunto 3: Mayor�a Jubilados
```
2 Normales
2 Estudiantes
15 Jubilados
Total: 19 pasajeros (LLENO)
Recaudaci�n esperada: $1,500
```

---

## ?? ATAJOS DE TECLADO

| Tecla | Acci�n |
|-------|--------|
| **F5** | Iniciar/Depurar |
| **Ctrl+F5** | Ejecutar sin depurar |
| **Shift+F5** | Detener depuraci�n |
| **F11** | Paso a paso (debug) |
| **Tab** | Moverse entre controles |
| **Enter** | Activar bot�n "Anotar" (si txtPasajero tiene foco) |

---

## ?? ESTRUCTURA DE ARCHIVOS

```
AppCombis/
?
??? Form1.cs                      # L�gica principal
??? Form1.Designer.cs             # Dise�o de UI
??? Pasajero.cs                   # Clase Pasajero
??? EstadisticasDiarias.cs        # Clase Estad�sticas
?
??? DOCUMENTACION_MEJORADA.md     # Doc t�cnica completa
??? CASOS_USO_DETALLADOS.md       # Casos de uso
??? RESUMEN_EJECUTIVO.md          # Resumen del proyecto
??? INICIO_RAPIDO.md              # Esta gu�a
?
??? README_SOLUCION.md            # Doc original
??? GUIA_DISENO_UI.md             # Gu�a de dise�o
??? CASOS_DE_PRUEBA.md            # Casos de prueba originales

Archivos generados (en bin\Debug\net8.0-windows\):
??? fila_espera.txt               # Persistencia de fila
??? estadisticas.txt              # Estad�sticas del d�a
??? Reporte_Combis_*.txt          # Reportes generados
```

---

## ?? CONCEPTOS CLAVE

### Cola (Queue)
```csharp
// FIFO: First-In, First-Out
Queue<Pasajero> fila = new Queue<Pasajero>();
fila.Enqueue(pasajero);  // Agregar al final
Pasajero p = fila.Dequeue();  // Quitar del inicio
```

### Lista (List)
```csharp
// Din�mica: Crece autom�ticamente
List<Pasajero> combi = new List<Pasajero>();
combi.Add(pasajero);  // Agregar
int cantidad = combi.Count;  // Contar
decimal total = combi.Sum(p => p.Tarifa);  // Sumar con LINQ
```

### Temporizador (Timer)
```csharp
timer.Interval = 1000;  // 1 segundo
timer.Tick += timerCombi_Tick;  // Evento
timer.Start();  // Iniciar
timer.Stop();  // Detener
```

---

## ?? TIPS Y TRUCOS

### ?? Para Pruebas R�pidas del Temporizador
Cambiar temporalmente en `Form1.cs`:
```csharp
// Original (20 minutos):
private const int TIEMPO_ESPERA_SEGUNDOS = 1200;

// Para pruebas (10 segundos):
private const int TIEMPO_ESPERA_SEGUNDOS = 10;
```

### ?? Para Ver M�s Detalles en el Reporte
Modificar `EstadisticasDiarias.GenerarReporte()` para agregar m�s informaci�n.

### ?? Para Cambiar Colores
Modificar valores RGB en `Form1.Designer.cs`:
```csharp
btnSubir.BackColor = Color.FromArgb(46, 125, 50);  // Verde
```

---

## ? CHECKLIST DE VERIFICACI�N

Antes de presentar/entregar:
- [ ] ? Compilaci�n exitosa (F5 sin errores)
- [ ] ? Todos los tipos de pasajero funcionan
- [ ] ? Temporizador inicia y cuenta correctamente
- [ ] ? Las 3 rutas se pueden seleccionar
- [ ] ? Reporte se genera y abre en Notepad
- [ ] ? Persistencia funciona (cerrar/abrir)
- [ ] ? Estad�sticas se actualizan en tiempo real
- [ ] ? Bot�n cerrar funciona con confirmaci�n
- [ ] ? Validaciones funcionan (capacidad, m�nimo)
- [ ] ? Documentaci�n est� completa

---

## ?? �LISTO PARA USAR!

Tu sistema est� completo y funcional. 

**Siguiente paso:**
1. Ejecutar con **F5**
2. Seguir "Primera Prueba: Viaje Simple"
3. Experimentar con diferentes escenarios
4. Generar varios reportes
5. Probar persistencia

**�Buena suerte! ????**

---

## ?? REFERENCIAS R�PIDAS

| Documento | Para Qu� |
|-----------|----------|
| `INICIO_RAPIDO.md` | Esta gu�a - Comenzar ahora |
| `DOCUMENTACION_MEJORADA.md` | Detalles t�cnicos completos |
| `CASOS_USO_DETALLADOS.md` | Pruebas paso a paso |
| `RESUMEN_EJECUTIVO.md` | Visi�n general del proyecto |

---

**�Todo listo! Presiona F5 y comienza a usar el sistema! ??**
