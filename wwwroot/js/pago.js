(function () {
    'use strict';

    
    var forms = document.querySelectorAll('.needs-validation');

    // Loop over them and prevent submission if they are invalid
    Array.prototype.slice.call(forms).forEach(function (form) {
        form.addEventListener('submit', function (event) {
            if (!form.checkValidity()) {
                event.preventDefault();
                event.stopPropagation();
            }

            form.classList.add('was-validated');
        }, false);
    });
})();

document.addEventListener('DOMContentLoaded', function () {
  
    function updateQuantity(button, operation) {
        const targetId = button.getAttribute('data-target');
        const quantityInput = document.getElementById(targetId);
        let currentValue = parseInt(quantityInput.value);

        if (operation === 'increase') {
            currentValue++;
        } else if (operation === 'decrease' && currentValue > 0) {
            currentValue--;
        }

        quantityInput.value = currentValue;
    }


    const increaseButtons = document.querySelectorAll('.increase');
    const decreaseButtons = document.querySelectorAll('.decrease');


    increaseButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            updateQuantity(button, 'increase');
        });
    });


    decreaseButtons.forEach(function (button) {
        button.addEventListener('click', function () {
            updateQuantity(button, 'decrease');
        });
    });
});

function controlarOpciones() {
    const pais = document.getElementById("pais").value;
    const facturaOption = document.getElementById("factura");

    if (pais === "Peru") {
        facturaOption.disabled = false;  
    } else if (pais === "Extranjero") {
        facturaOption.disabled = true;  
        document.getElementById("boleta").checked = true; 
        mostrarCampos();  
    } else {
        facturaOption.disabled = true;  
    }
}

function mostrarCampos() {
    const boletaChecked = document.getElementById("boleta").checked;
    const boletaCampos = document.getElementById("boletaCampos");
    const facturaCampos = document.getElementById("facturaCampos");

    if (boletaChecked) {
        boletaCampos.style.display = "block";
        facturaCampos.style.display = "none";
    } else {
        boletaCampos.style.display = "none";
        facturaCampos.style.display = "block";
    }
}
