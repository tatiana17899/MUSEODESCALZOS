// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
@section Scripts {
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <script>
        var ctx = document.getElementById('calificacionesChart').getContext('2d');
        var calificacionesChart = new Chart(ctx, {
            type: 'pie',
            data: {
                labels: ['Corazones', 'Sonrisas', 'Tristeza'],
                datasets: [{
                    data: [120, 35, 2], // Aquí puedes cambiar los valores según las calificaciones reales
                    backgroundColor: ['#FF6384', '#FFCE56', '#36A2EB'],
                }]
            },
            options: {
                responsive: true,
                plugins: {
                    legend: {
                        position: 'top',
                    },
                    title: {
                        display: true,
                        text: 'Calificaciones de Noticias'
                    }
                }
            }
        });
    </script>
}
