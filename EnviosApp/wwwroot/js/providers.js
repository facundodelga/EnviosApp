// Verificación de rol al cargar la página
document.addEventListener('DOMContentLoaded', function() {
    // Obtén el token JWT del localStorage
    const jwt = localStorage.getItem('jwt');

    // Redirigir si no hay token
    if (!jwt) {
        alert('No tienes acceso a esta página.');
        window.location.href = '/index.html';
    }

    // Decodificar el JWT y verificar el rol
/*     try {
        const payload = JSON.parse(atob(token.split('.')[1]));
        if (payload.role !== 'admin') {
            alert('Acceso no autorizado');
            window.location.href = 'home.html';
            return;
        }
    } catch (e) {
        console.error('Error al verificar el token:', e);
        window.location.href = 'index.html';
    } */

    loadProviders();
});

// Cargar proveedores
async function loadProviders() {
    try {
        const response = await fetch('/api/providers', {
            headers: {
                'Authorization': `Bearer ${localStorage.getItem('token')}`
            }
        });
        const providers = await response.json();
        displayProviders(providers);
    } catch (error) {
        console.error('Error al cargar proveedores:', error);
        alert('Error al cargar los proveedores');
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
            <td>${formatServices(provider.services)}</td>
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

// Formatear servicios para mostrar en la tabla
function formatServices(services) {
    return services.map(s => `${s.name} (${s.multiplier}x)`).join(', ');
}

// Formatear zonas para mostrar en la tabla
function formatZones(zones) {
    return zones.map(z => `${z.name}: ${z.basePrice}`).join(', ');
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

// Guardar proveedor
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
}

// Cerrar sesión
function logout() {
    localStorage.removeItem('jwt');
    window.location.href = '/index.html';
}