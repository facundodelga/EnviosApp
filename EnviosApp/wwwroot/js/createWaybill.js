document.addEventListener("DOMContentLoaded", () => {
    loadSelect("clientId", "/api/clients");
    loadSelect("serviceTypeId", "/api/servicetypes");
    loadSelect("providerId", "/api/providers");
    loadSelect("countryId", "/api/countries");
  
    document.getElementById("guiaForm").addEventListener("submit", async (e) => {
      e.preventDefault();
  
      const data = {
        waybillNumber: document.getElementById("waybillNumber").value,
        description: document.getElementById("description").value,
        name: document.getElementById("name").value,
        organization: document.getElementById("organization").value,
        address: document.getElementById("address").value,
        city: document.getElementById("city").value,
        zipCode: document.getElementById("zipCode").value,
        phone: document.getElementById("phone").value,
        email: document.getElementById("email").value,
        date: document.getElementById("date").value,
        weight: parseFloat(document.getElementById("weight").value),
        width: parseFloat(document.getElementById("width").value),
        height: parseFloat(document.getElementById("height").value),
        depth: parseFloat(document.getElementById("depth").value),
        price: parseFloat(document.getElementById("price").value),
        clientId: parseInt(document.getElementById("clientId").value),
        serviceTypeId: parseInt(document.getElementById("serviceTypeId").value),
        providerId: parseInt(document.getElementById("providerId").value),
        countryId: parseInt(document.getElementById("countryId").value),
      };
  
      const res = await fetch("/api/waybills", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(data)
      });
  
      if (res.ok) {
        alert("Guía creada con éxito");
        document.getElementById("guiaForm").reset();
      } else {
        alert("Error al crear guía");
      }
    });
  });
  
  async function loadSelect(id, url) {
    const res = await fetch(url);
    const data = await res.json();
    const select = document.getElementById(id);
    data.forEach(item => {
      const option = document.createElement("option");
      option.value = item.id;
      option.textContent = item.name;
      select.appendChild(option);
    });
  }
  