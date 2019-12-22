/*******Validacion de Fomularios********/
(function () {
    'use strict';
    window.addEventListener('load', function () {
        // Obtengo todos los formularios a los que queremos aplicar estilos personalizados de validación Bootstrap
        var forms = document.getElementsByClassName('needs-validation');

        var validation = Array.prototype.filter.call(forms, function (form) {
            form.addEventListener('submit', function (event) {
                if (form.checkValidity() === false) {
                    event.preventDefault();
                    event.stopPropagation();
                }
                form.classList.add('was-validated');
            }, false);
        });
    }, false);
})();

/************Menu Flotante**************/
$(function () {
    var boton = $('#btn-menu');
    var fondo_enlace = $('#fondo_enlace');

    boton.on('click', function (e) {
        fondo_enlace.toggleClass('active');
        $('#barra-lateral-izquierda').toggleClass('active');
        e.preventDefault();
    })

    fondo_enlace.on('click', function (e) {
        fondo_enlace.toggleClass('active');
        $('#barra-lateral-izquierda').toggleClass('active');
        e.preventDefault();
    })
}())


/************** ToolTips *****************/    
$(function () {
    $('[data-toggle="tooltip"]').tooltip();
})
    