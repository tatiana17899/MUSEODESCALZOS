const stripe = Stripe(
  "pk_test_51QDeapAc8IjLns8yDzX1HWHe2T2O3joYWJpnO3lhGWSIvlojLsJkJn7dK14PXw64YtWzxUdQ1nKNWYPxadBi5AY900tEQUTxfS"
);

const elements = stripe.elements();
const cardElement = elements.create("card");
cardElement.mount("#card-element");

const form = document.getElementById("payment-form");

form.addEventListener("submit", function (event) {
  event.preventDefault();

  Swal.fire({
    title: "¿Estás seguro de realizar el pago?",
    text: "Una vez confirmado, no podrás revertir esta acción.",
    icon: "warning",
    showCancelButton: true,
    confirmButtonColor: "#3085d6",
    cancelButtonColor: "#d33",
    confirmButtonText: "Sí, confirmar pago",
    cancelButtonText: "Cancelar",
  }).then((result) => {
    if (result.isConfirmed) {
      stripe.createToken(cardElement).then(function (result) {
        if (result.error) {
          // Mostrar mensaje de error
          document.getElementById("card-errors").textContent =
            result.error.message;
        } else {
          // Crear un input oculto para el token
          var hiddenInput = document.getElementById("stripeToken");
          hiddenInput.setAttribute("value", result.token.id);
          form.appendChild(hiddenInput);

          fetch(form.action, {
            method: "POST",
            body: new FormData(form),
          })
            .then((response) => {
              if (response.ok) {
                return response.json(); // Analiza la respuesta JSON
              } else {
                // Maneja el error y lanza una excepción para que se capture en el catch
                return response.json().then((errorData) => {
                  throw new Error(errorData.message || "Error en la solicitud");
                });
              }
            })
            .then((data) => {
              window.location.href = data.redirectUrl;
            })
            .catch((error) => {
              console.error("Error en la solicitud:", error);
              Swal.fire({
                icon: "error",
                title: "Error",
                text: "Ocurrió un error al enviar la solicitud.",
              });
            });
        }
      });
    }
  });
});
