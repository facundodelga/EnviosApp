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

async function loadCountries() {
    try {
        const response = await fetch('/api/countries', {
            headers: { 'Authorization': `Bearer ${jwt}` }
        });
        const countries = await response.json();

        document.querySelectorAll('.country-select').forEach(select => {
            select.innerHTML = '<option value="" disabled selected>Seleccione un país</option>';
            countries.forEach(country => {
                const option = document.createElement('option');
                option.value = country.alpha;  // Código Alpha como value
                option.textContent = country.name;  // Nombre del país
                option.dataset.countryId = country.id; // ✅ Guardar ID en dataset
                select.appendChild(option);
            });
        });
    } catch (error) {
        console.error('Error al cargar países:', error);
        showToast('Error al cargar los países', 'error');
    }
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
                        <select class="form-select country-select" required>
                            <option value="" disabled selected>Seleccione un país</option>
                        </select>
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
    const zoneEntry = button.closest('.zone-entry');
    const countriesContainer = zoneEntry.querySelector('.countries-container');

    if (!countriesContainer) {
        console.error('No se encontró el contenedor de países para agregar.');
        return;
    }

    // Crear una nueva fila con un select de Select2
    const newRow = document.createElement('div');
    newRow.className = 'row mb-2';
    newRow.innerHTML = `
        <div class="col-md-11">
            <select class="form-control country-select" style="width: 100%;" required></select>
        </div>
        <div class="col-md-1">
            <button type="button" class="btn btn-danger" onclick="removeCountry(this)">
                <i class="bi bi-trash"></i>
            </button>
        </div>
    `;

    // Agregar la nueva fila al contenedor
    countriesContainer.appendChild(newRow);

    // Inicializar Select2 en el nuevo select
    const newSelect = newRow.querySelector('.country-select');
    loadCountriesForSelect(newSelect);
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

// Mostrar formulario para nuevo proveedor
async function showAddProviderForm() {
    document.getElementById('providerForm').reset();
    document.getElementById('providerForm').removeAttribute('data-provider-id');
    document.getElementById('modalTitle').textContent = 'Nuevo Proveedor';
    
    // Limpiar formularios y agregar una entrada inicial
    document.getElementById('servicesContainer').innerHTML = '';
    document.getElementById('zonesContainer').innerHTML = '';
    addService();
    addZone();

    // Cargar lista de países
    await loadCountries();

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

        console.log('Datos del proveedor:', provider);

        // Cargar países y servicios antes de asignar valores
        await loadCountries();

        // Configurar datos del proveedor
        document.getElementById('name').value = provider.name;
        document.getElementById('providerForm').dataset.providerId = providerId;
        document.getElementById('modalTitle').textContent = 'Editar Proveedor';

        // Limpiar servicios y zonas
        document.getElementById('servicesContainer').innerHTML = '';
        document.getElementById('zonesContainer').innerHTML = '';

        // Cargar servicios
        provider.serviceTypes.forEach(service => {
            addService();

            const lastService = document.querySelector('.service-entry:last-child');
            if (lastService) {
                lastService.dataset.serviceId = service.id; // Guardar ID del servicio
                const inputs = lastService.querySelectorAll('input');
                if (inputs[0]) inputs[0].value = service.name;
                if (inputs[1]) inputs[1].value = service.priceMultiplier;
            }
        });

        // Cargar zonas y países
        provider.zones.forEach(zone => {
            addZone();

            const lastZone = document.querySelector('.zone-entry:last-child');
            lastZone.dataset.zoneId = zone.id; // Guardar ID de la zona

            const zoneInputs = lastZone.querySelectorAll('.row input');
            if (zoneInputs[0]) zoneInputs[0].value = zone.name;
            if (zoneInputs[1]) zoneInputs[1].value = zone.basePrice;

            const countryContainer = lastZone.querySelector('.countries-container');
            if (!countryContainer) return;

            countryContainer.innerHTML = '';

            zone.countries.forEach(country => {
                addCountry(lastZone.querySelector('.btn-secondary'));
                const lastCountryInput = countryContainer.querySelector('.row:last-child .country-select');

                if (lastCountryInput) {
                    // Inicializar Select2 con el país seleccionado
                    loadCountriesForSelect(lastCountryInput, country.alpha);
                }
            });
        });

        // Mostrar el modal
        const modal = new bootstrap.Modal(document.getElementById('providerModal'));
        modal.show();
    } catch (error) {
        console.error('Error al cargar el proveedor:', error);
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




async function loadCountriesForSelect(selectElement, selectedAlpha = null) {
    // Inicializar Select2 en el select
    $(selectElement).select2({
        placeholder: "Buscar país...",
        allowClear: true,
        dropdownParent: $(selectElement).closest('.modal'), // Renderizar dentro del modal
        ajax: {
            url: '/api/countries', // Endpoint para cargar países
            headers: {
                'Authorization': `Bearer ${jwt}`
            },
            dataType: 'json',
            delay: 250, // Retardo para evitar muchas solicitudes
            data: function (params) {
                return {
                    search: params.term, // Término de búsqueda
                };
            },
            processResults: function (data, params) {
                // Procesar los resultados de la API
                return {
                    results: data.map(country => ({
                        id: country.alpha, // Código alpha del país
                        text: country.name // Nombre del país
                    })),
                    pagination: {
                        more: false // No hay paginación en este ejemplo
                    }
                };
            },
            cache: true
        },
        minimumInputLength: 1 // Mínimo de caracteres para realizar la búsqueda
    });

    // Si hay un país seleccionado, establecerlo como valor
    if (selectedAlpha) {
        $(selectElement).val(selectedAlpha).trigger('change');
    }
}


async function saveProvider() {
    const form = document.getElementById('providerForm');
    if (!form.checkValidity()) {
        form.reportValidity();
        return;
    }

    const isEditing = !!document.getElementById('providerForm').dataset.providerId;
    const providerId = isEditing ? document.getElementById('providerForm').dataset.providerId : null;

    const providerData = {
        id: providerId,
        name: document.getElementById('name').value,
        serviceTypes: [],
        zones: []
    };

    // Obtener servicios
    document.querySelectorAll('.service-entry').forEach(serviceEl => {
        const inputs = serviceEl.querySelectorAll('input');
        providerData.serviceTypes.push({
            id: serviceEl.dataset.serviceId || null,
            name: inputs[0].value,
            priceMultiplier: parseFloat(inputs[1].value)
        });
    });

    // Obtener zonas y países
    document.querySelectorAll('.zone-entry').forEach(zoneEl => {
        const zoneId = zoneEl.dataset.zoneId || null;
        const zoneInputs = zoneEl.querySelectorAll('.row input');
        const countries = [];

        zoneEl.querySelectorAll('.country-select').forEach(countrySelect => {
            const selectedOption = countrySelect.options[countrySelect.selectedIndex];

            if (selectedOption) {
                countries.push({
                    
                    name: selectedOption.text,  
                    alpha: selectedOption.value  
                });
            }
        });

        providerData.zones.push({
            id: zoneId,
            name: zoneInputs[0].value,
            basePrice: parseFloat(zoneInputs[1].value),
            countries: countries
        });
    });

    console.log('Datos enviados al backend:', JSON.stringify(providerData, null, 2));

    try {
        const url = isEditing 
            ? `/api/provider/${providerId}`
            : '/api/provider';
        
        const response = await fetch(url, {
            method: isEditing ? 'PUT' : 'POST',
            headers: {
                'Content-Type': 'application/json',
                'Authorization': `Bearer ${jwt}`
            },
            body: JSON.stringify(providerData)
        });

        if (!response.ok) {
            const errorMessage = await response.text();
            throw new Error(`Error en la respuesta del servidor: ${errorMessage}`);
        }

        bootstrap.Modal.getInstance(document.getElementById('providerModal')).hide();
        loadProviders();
        showToast(isEditing ? 'Proveedor actualizado con éxito' : 'Proveedor creado con éxito', 'success');
    } catch (error) {
        console.error('Error al guardar el proveedor:', error);
        showToast('Error al guardar el proveedor', 'error');
    }
}