# Prueba Técnica Full Stack - Procesamiento de PDFs

Este proyecto implementa una solución completa con:

- ✅ API REST en ASP.NET Core (`ApiPruebaTecnica`)
- ✅ Servicio de procesamiento en consola (.NET) (`PdfProcessorApp`)
- ✅ Interfaz web (HTML + Bootstrap + JS) (`Frontend`)

---

## 📁 Estructura del proyecto

/PruebaTecnicaLaura/
├── ApiPruebaTecnica/        ← Backend API REST
├── PdfProcessorApp/         ← Servicio de procesamiento de PDFs
├── Frontend/                ← Interfaz web
└── README.md                ← Instrucciones

---

## 🚀 Requisitos

- .NET 6 SDK o superior (https://dotnet.microsoft.com/download)
- Navegador moderno (Chrome, Edge, Firefox)
- Visual Studio / VS Code (opcional)

---

## 🔧 Configuración

### 1. API REST (`ApiPruebaTecnica`)

cd ApiPruebaTecnica  
dotnet restore  
dotnet run

> La API se ejecutará en `http://localhost:5000` por defecto.

---

### 2. Servicio de procesamiento (`PdfProcessorApp`)

Este servicio:

- Lee PDFs de `C:\PruebaEQ`
- Busca coincidencias con palabras clave
- Mueve los archivos a `OCR/` o `UNKNOWN/`
- Registra los resultados en la API

cd PdfProcessorApp  
dotnet restore  
dotnet run

---

### 3. Interfaz Web (`Frontend`)

Abre el archivo `Frontend/index.html` en un navegador. Desde ahí puedes:

- Subir archivos PDF
- Agregar/Eliminar claves
- Ver el historial de procesamiento

---

## ✅ Prueba completada

Con este proyecto se cumple todo lo solicitado:

- API con endpoints CRUD y carga de archivos
- Procesamiento automático de PDFs con claves
- Interfaz clara y funcional

---

Desarrollado para la prueba técnica de Desarrollador Full Stack.
