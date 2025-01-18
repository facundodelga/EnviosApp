// Verificación de rol al cargar la página
// Obtén el token JWT del localStorage
const jwt = localStorage.getItem('jwt');
document.addEventListener('DOMContentLoaded', function() {
    

    // Redirigir si no hay token
    if (!jwt) {
        alert('No tienes acceso a esta página.');
        window.location.href = '/index.html';
    }

    // Decodificar el JWT y verificar el rol
    
    const decodedToken = parseJwt(jwt);
    
    if (decodedToken.role !== 'admin') {
        alert('Acceso no autorizado');
        window.location.href = '/home.html';
        return;
    }   

    loadProviders();
});

// Función para decodificar el token JWT
function parseJwt(token) {
    try {
        const base64Url = token.split('.')[1];
        const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/');
        return JSON.parse(window.atob(base64));
    } catch (e) {
        return null;
    }
}

// Cargar proveedores
async function loadProviders() {
    try {
        const response = await fetch('/api/provider', {
            headers: {
                'Authorization': `Bearer ${jwt}`
            }
        });
        const providers = await response.json();
        
        //console.log('Estructura de la respuesta:', JSON.stringify(providers, null, 2));
        
        displayProviders(providers);
    } catch (error) {
        console.error('Error al cargar proveedores:', error);
        
        alert('Error al cargar los proveedores ', error);
        window.location.href = '/home.html';
        
    }
}

// Mostrar proveedores en la tabla
function displayProviders(providers) {
    const tbody = document.querySelector('#providersTable tbody');
    tbody.innerHTML = '';

    providers.forEach(provider => {
        const tr = document.createElement('tr');
        tr.innerHTML = `
            <td>${provider.name}</td>
            <td>${formatServices(provider.serviceTypes)}</td>
            <td>${formatZones(provider.zones)}</td>
            <td>
                <button class="btn btn-sm btn-primary" onclick="editProvider(${provider.id})">
                    <i class="bi bi-pencil"></i>
                </button>
                <button class="btn btn-sm btn-danger" onclick="deleteProvider(${provider.id})">
                    <i class="bi bi-trash"></i>
                </button>
            </td>
        `;
        tbody.appendChild(tr);
    });
}
/*
  {
    "id": 1,
    "name": "DHL",
    "zones": [
      {
        "id": 1,
        "name": "Zona 1",
        "basePrice": 2
      }
    ],
    "serviceTypes": [
      {
        "id": 1,
        "name": "DHL Economy",
        "priceMultiplier": 0.5
      }
    ]
  }
]
*/ 

// Formatear servicios para mostrar en la tabla
function formatServices(services) {
    return services.map(s => `${s.name} (${s.priceMultiplier}x)`).join(', ');
}

// Formatear zonas para mostrar en la tabla
function formatZones(zones) {
    return zones.map(z => `${z.name}: $${z.basePrice}`).join(', ');
}

// Agregar nuevo servicio
function addService() {
    const container = document.getElementById('servicesContainer');
    const serviceDiv = document.createElement('div');
    serviceDiv.className = 'row mb-2 service-entry';
    serviceDiv.innerHTML = `
        <div class="col-md-8">
            <input type="text" class="form-control" placeholder="Nombre del servicio" required>
        </div>
        <div class="col-md-3">
            <input type="number" step="0.01" class="form-control" placeholder="Multiplicador" required>
        </div>
        <div class="col-md-1">
            <button type="button" class="btn btn-danger" onclick="removeService(this)">
                <i class="bi bi-trash"></i>
            </button>
        </div>
    `;
    container.appendChild(serviceDiv);
}

// Agregar nueva zona
function addZone() {
    const container = document.getElementById('zonesContainer');
    const zoneDiv = document.createElement('div');
    zoneDiv.className = 'card mb-3 zone-entry';
    zoneDiv.innerHTML = `
        <div class="card-body">
            <div class="row mb-2">
                <div class="col-md-6">
                    <input type="text" class="form-control" placeholder="Nombre de la zona" required>
                </div>
                <div class="col-md-5">
                    <input type="number" step="0.01" class="form-control" placeholder="Precio base" required>
                </div>
                <div class="col-md-1">
                    <button type="button" class="btn btn-danger" onclick="removeZone(this)">
                        <i class="bi bi-trash"></i>
                    </button>
                </div>
            </div>
            <div class="countries-container">
                <div class="row mb-2">
                    <div class="col-md-11">
                        <input type="text" class="form-control" placeholder="País" required>
                    </div>
                    <div class="col-md-1">
                        <button type="button" class="btn btn-danger" onclick="removeCountry(this)">
                            <i class="bi bi-trash"></i>
                        </button>
                    </div>
                </div>
            </div>
            <button type="button" class="btn btn-secondary btn-sm" onclick="addCountry(this)">
                <i class="bi bi-plus"></i> Agregar País
            </button>
        </div>
    `;
    container.appendChild(zoneDiv);
}

// Agregar país a una zona
function addCountry(button) {
    const container = button.previousElementSibling;
    const countryDiv = document.createElement('div');
    countryDiv.className = 'row mb-2';
    countryDiv.innerHTML = `
        <div class="col-md-11">
            <input type="text" class="form-control" placeholder="País" required>
        </div>
        <div class="col-md-1">
            <button type="button" class="btn btn-danger" onclick="removeCountry(this)">
                <i class="bi bi-trash"></i>
            </button>
        </div>
    `;
    container.appendChild(countryDiv);
}

// Funciones para eliminar elementos
function removeService(button) {
    button.closest('.service-entry').remove();
}

function removeZone(button) {
    button.closest('.zone-entry').remove();
}

function removeCountry(button) {
    button.closest('.row').remove();
}

async function saveProvider() {
    const form = document.getElementById('providerForm');
    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const providerData = {
        name: document.getElementById('name').value,
        services: [],
        zones: []
    };

    // Recolectar servicios
    document.querySelectorAll('.service-entry').forEach(serviceEl => {
        const inputs = serviceEl.querySelectorAll('input');
        providerData.services.push({
            name: inputs[0].value,
            multiplier: parseFloat(inputs[1].value)
        });
    });

    // Recolectar zonas y países
    document.querySelectorAll('.zone-entry').forEach(zoneEl => {
        const zoneInputs = zoneEl.querySelectorAll('.card-body > .row input');
        const countries = [];
        
        zoneEl.querySelectorAll('.countries-container .row input').forEach(countryInput => {
            countries.push(countryInput.value);
        });

        providerData.zones.push({
            name: zoneInputs[0].value,
            basePrice: parseFloat(zoneInputs[1].value),
            countries: countries
        });
    });

    try {
        const isEditing = !!document.getElementById('providerForm').dataset.providerId;
        const url = isEditing 
            ? `/api/provider/${document.getElementById('providerForm').dataset.providerId}`
            : '/api/provider';
        
        const response = await fetch(url, {
            method: isEditing ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwt}`
            },
            body: JSON.stringify(providerData)
        });

        if (!response.ok) throw new Error('Error en la respuesta del servidor');

        bootstrap.Modal.getInstance(document.getElementById('providerModal')).hide();
        loadProviders();
        showToast(isEditing ? 'Proveedor actualizado con éxito' : 'Proveedor creado con éxito', 'success');
    } catch (error) {
        console.error('Error al guardar el proveedor:', error);
        showToast('Error al guardar el proveedor', 'error');
    }
}

// Mostrar formulario para nuevo proveedor
function showAddProviderForm() {
    document.getElementById('providerForm').reset();
    document.getElementById('providerForm').removeAttribute('data-provider-id');
    document.getElementById('modalTitle').textContent = 'Nuevo Proveedor';
    
    // Limpiar formulario
    document.getElementById('servicesContainer').innerHTML = '';
    document.getElementById('zonesContainer').innerHTML = '';
    
    // Agregar una entrada inicial para servicio y zona
    addService();
    addZone();
    
    const modal = new bootstrap.Modal(document.getElementById('providerModal'));
    modal.show();
}

// Editar proveedor existente
async function editProvider(providerId) {
    try {
        const response = await fetch(`/api/provider/${providerId}`, {
            headers: {
                'Authorization': `Bearer ${jwt}`
            }
        });
        const provider = await response.json();

        // Configurar formulario
        document.getElementById('name').value = provider.name;
        document.getElementById('providerForm').dataset.providerId = providerId;
        document.getElementById('modalTitle').textContent = 'Editar Proveedor';

        // Limpiar contenedores existentes
        document.getElementById('servicesContainer').innerHTML = '';
        document.getElementById('zonesContainer').innerHTML = '';

        // Cargar servicios
        provider.serviceTypes.forEach(service => {
            addService();
            const serviceEntry = document.querySelector('.service-entry:last-child');
            const inputs = serviceEntry.querySelectorAll('input');
            inputs[0].value = service.name;
            inputs[1].value = service.priceMultiplier;
        });

        // Cargar zonas y países
        provider.zones.forEach(zone => {
            addZone();
            const zoneEntry = document.querySelector('.zone-entry:last-child');
            const zoneInputs = zoneEntry.querySelectorAll('.card-body > .row input');
            zoneInputs[0].value = zone.name;
            zoneInputs[1].value = zone.basePrice;
            /*
            // Limpiar países por defecto
            zoneEntry.querySelector('.countries-container').innerHTML = '';

            // Agregar países
            zone.countries.forEach(country => {
                addCountry(zoneEntry.querySelector('.btn-secondary'));
                const lastCountryInput = zoneEntry.querySelector('.countries-container .row:last-child input');
                lastCountryInput.value = country;
            });
            */
        });

        const modal = new bootstrap.Modal(document.getElementById('providerModal'));
        modal.show();
    } catch (error) {
        console.error('Error al cargar el proveedor:', error);
        showToast('Error al cargar el proveedor', 'error');
    }
}

// Eliminar proveedor
async function deleteProvider(providerId) {
    if (!confirm('¿Está seguro de que desea eliminar este proveedor?')) {
        return;
    }

    try {
        const response = await fetch(`/api/provider/${providerId}`, {
            method: 'DELETE',
            headers: {
                'Authorization': `Bearer ${jwt}`
            }
        });

        if (!response.ok) throw new Error('Error en la respuesta del servidor');

        loadProviders();
        showToast('Proveedor eliminado con éxito', 'success');
    } catch (error) {
        console.error('Error al eliminar el proveedor:', error);
        showToast('Error al eliminar el proveedor', 'error');
    }
}

// Búsqueda de proveedores
function searchProviders() {
    const searchTerm = document.getElementById('searchInput').value.toLowerCase();
    const rows = document.querySelectorAll('#providersTable tbody tr');

    rows.forEach(row => {
        const text = row.textContent.toLowerCase();
        row.style.display = text.includes(searchTerm) ? '' : 'none';
    });
}

// Mostrar mensajes toast
function showToast(message, type = 'info') {
    const toastContainer = document.createElement('div');
    toastContainer.className = 'position-fixed bottom-0 end-0 p-3';
    toastContainer.style.zIndex = '11';
    
    toastContainer.innerHTML = `
        <div class="toast align-items-center text-white bg-${type === 'success' ? 'success' : 'danger'}" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="d-flex">
                <div class="toast-body">
                    ${message}
                </div>
                <button type="button" class="btn-close btn-close-white me-2 m-auto" data-bs-dismiss="toast"></button>
            </div>
        </div>
    `;
    
    document.body.appendChild(toastContainer);
    const toast = new bootstrap.Toast(toastContainer.querySelector('.toast'));
    toast.show();
    
    toast._element.addEventListener('hidden.bs.toast', () => {
        toastContainer.remove();
    });
}

// Cerrar sesión
function logout() {
    localStorage.removeItem('jwt');
    window.location.href = '/index.html';
}