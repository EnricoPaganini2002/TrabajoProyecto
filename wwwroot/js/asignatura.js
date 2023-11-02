window.onload = function () {
    BuscarAsignaturas();
}


function BuscarAsignaturas() {
    $("#tbody-asignatura").empty();
    $.ajax({
        // la URL para la petición
        url: '../../Asignaturas/BuscarAsignaturas',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (asignaturas) {

            $("#tbody-asignatura").empty();
            $.each(asignaturas, function (Index, asignatura) {
                //VARIABLES PARA DEFINIR BOTONES Y ESTETICA
                let BotonDesahabilitar = '';
                let botones = '<button type="button" onclick="BuscarAsignatura(' + asignatura.asignaturaID + ')" class="btn btn-primary btn-sm" style="margin-right:5px" onkeyup="this.value = this.value.toUpperCase()">Editar</button>' +
                    '<button type="button" onclick="DesahabilitarAsignatura(' + asignatura.asignaturaID + ',1)" class="btn btn-danger btn-sm">Desahabilitar</button>';
                //DEFINE SI ESTA ELIMINADA
                if (asignatura.eliminar) {
                    BotonDesahabilitar = 'table-danger';
                    botones = '<button type="button" onclick="DesahabilitarAsignatura('
                        + asignatura.asignaturaID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                }
                $("#tbody-asignatura").append('<tr class=' + BotonDesahabilitar + '>'
                    + '<td>' + asignatura.nombreAsignatura + '</td>'
                    + '<td>' + asignatura.nombreCarrera + '</td>'
                    + '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert('Error al cargar asignaturas');
        },

        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('Petición realizada');
        }
    });
}






function VaciarFormulario() {
    $("#AsignaturaID").val(0);
    $("#NombreAsignatura").val('');
    $("#CarreraID").val('');
}

function BuscarAsignatura(AsignaturaID) {
    // $("#Titulo-Modal-Servicio").text("Editar Subcategoria");
    $("#AsignaturaID").val(AsignaturaID);
    $.ajax({
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',

        url: '../../Asignaturas/BuscarAsignaturas',
        data: { AsignaturaID: AsignaturaID },
        success: function (asignaturas) {
            // console.log(servicios);
            if (asignaturas.length == 1) {
                let asignatura = asignaturas[0];
                // console.log(servicio);
                $("#NombreAsignatura").val(asignatura.nombreAsignatura);
                $("#CarreraID").val(asignatura.carreraID);
                $("#ModalAsignatura").modal("show");
            }
        },
        error: function (data) {
        }
    });
}

function GuardarAsignatura() {
    //JAVASCRIPT
    let asignaturaID = $("#AsignaturaID").val();
    let nombreAsignatura = $("#NombreAsignatura").val();
    let carreraID = $("#CarreraID").val();

    // var nombreAsignatura = document.getElementById("NombreAsignatura").value;
    // //Llamar a todos los input
    // var carreraID1 = document.getElementById("CarreraID").value;


    // if (nombreAsignatura.trim() === "") {
    //     alert("El campo 'Nombre Carrera' es obligatorio.");
    //     return;
    // }
    // if (carreraID1.trim() === "") {
    //     alert("El campo 'Duracion' es obligatorio.");
    //     return;
    // }
    $.ajax({
        // la URL para la petición
        url: '../../Asignaturas/GuardarAsignatura',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { AsignaturaID: asignaturaID, NombreAsignatura: nombreAsignatura, CarreraID: carreraID },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                $("#ModalAsignatura").modal("hide");
                BuscarAsignaturas();
            }
            else {
                alert("Existe una Carrera con el mismo Nombre.");
            }
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}

function DesahabilitarAsignatura(AsignaturaID, Eliminar) {
    $.ajax({
        // la URL para la petición
        url: '../../Asignaturas/DesahabilitarAsignatura',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { AsignaturaID: AsignaturaID, Eliminar: Eliminar },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                BuscarAsignaturas();

            }
            else {
                // Mostrar la alerta de error si no se puede deshabilitar la carrera
                window.alert("No se puede deshabilitar la carrera, hay asignaturas asociadas activas.");
            }
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }

    });

}


