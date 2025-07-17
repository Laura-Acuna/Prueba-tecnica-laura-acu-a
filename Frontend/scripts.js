const apiUrl = "http://localhost:5000/api"; // Ajusta el puerto si es diferente

document.getElementById("uploadForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const file = document.getElementById("pdfFile").files[0];
    const result = document.getElementById("uploadResult");

    if (!file || file.type !== "application/pdf") {
        result.textContent = "Solo se permiten archivos PDF.";
        result.className = "text-danger";
        return;
    }

    if (file.size > 10 * 1024 * 1024) {
        result.textContent = "Tamaño máximo excedido (10MB).";
        result.className = "text-danger";
        return;
    }

    const formData = new FormData();
    formData.append("file", file);

    try {
        const res = await fetch(`${apiUrl}/fileupload`, {
            method: "POST",
            body: formData,
        });
        const text = await res.text();
        result.textContent = text;
        result.className = res.ok ? "text-success" : "text-danger";
    } catch (err) {
        result.textContent = "Error al subir archivo.";
        result.className = "text-danger";
    }
});

document.getElementById("keyForm").addEventListener("submit", async (e) => {
    e.preventDefault();
    const clave = document.getElementById("clave").value;
    const docName = document.getElementById("docName").value;

    const res = await fetch(`${apiUrl}/dockey`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ clave, docName })
    });

    if (res.ok) {
        loadKeys();
        e.target.reset();
    }
});

async function loadKeys() {
    const res = await fetch(`${apiUrl}/dockey`);
    const keys = await res.json();
    const table = document.querySelector("#keysTable tbody");
    table.innerHTML = "";

    keys.forEach(k => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${k.clave}</td>
            <td>${k.docName}</td>
            <td><button class="btn btn-sm btn-danger" onclick="deleteKey('${k.id}')">Eliminar</button></td>
        `;
        table.appendChild(tr);
    });
}

async function deleteKey(id) {
    await fetch(`${apiUrl}/dockey/${id}`, { method: "DELETE" });
    loadKeys();
}

async function loadLogs() {
    const res = await fetch(`${apiUrl}/log`);
    const logs = await res.json();
    const table = document.querySelector("#logsTable tbody");
    table.innerHTML = "";

    logs.forEach(log => {
        const tr = document.createElement("tr");
        tr.innerHTML = `
            <td>${log.nombreOriginal}</td>
            <td>${log.nuevoNombre ?? "-"}</td>
            <td>${log.estado}</td>
            <td>${new Date(log.fechaProcesamiento).toLocaleString()}</td>
        `;
        table.appendChild(tr);
    });
}

loadKeys();
loadLogs();