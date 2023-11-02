window.onload = BuscarCarreras();

function BuscarCarreras() {
    $("#tbody-carreras").empty();
    $.ajax({
        // la URL para la petición
        url: '../../Carreras/BuscarCarreras',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (carreras) {

            $("#tbody-carreras").empty();
            $.each(carreras, function (Index, carrera) {
                //VARIABLES PARA DEFINIR BOTONES Y ESTETICA
                        let BotonDesahabilitar = '';
                let botones = '<button type="button" onclick="BuscarCarrera(' + carrera.carreraID + ')" class="btn btn-primary btn-sm" style="margin-right:5px" onkeyup="this.value = this.value.toUpperCase()">Editar</button>' +
                            '<button type="button" onclick="DesahabilitarCarrera(' + carrera.carreraID + ',1)" class="btn btn-danger btn-sm">Desahabilitar</button>';
                        //DEFINE SI ESTA ELIMINADA
                        if (carrera.eliminar) {
                            BotonDesahabilitar = 'table-danger';
                            botones = '<button type="button" onclick="DesahabilitarCarrera('
                                + carrera.carreraID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                        }
                $("#tbody-carreras").append('<tr class=' + BotonDesahabilitar + '>'
                + '<td>' + carrera.nombreCarrera + '</td>'
                + '<td>' + carrera.duracion + ' AÑOS' + '</td>'
                + '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert('Error al cargar carreras');
        },

        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('Petición realizada');
        }
    });
}
// // Supongamos que esta variable contiene los roles del usuario
// var rolesDelUsuario = ["Administrador", "Profesor"];

// // Función para verificar si el usuario tiene un rol específico
// function UserIsInRole(role) {
//     // Verificar si el rol especificado está en la lista de roles del usuario
//     return rolesDelUsuario.includes(role);
// }


// function BuscarCarreras() {
//     $("#tbody-carreras").empty();
//     $.ajax({
//         // la URL para la petición
//         url: '../../Carreras/BuscarCarreras',
//         // la información a enviar
//         // (también es posible utilizar una cadena de datos)
//         data: {},
//         // especifica si será una petición POST o GET
//         type: 'GET',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',
//         // código a ejecutar si la petición es satisfactoria;
//         // la respuesta es pasada como argumento a la función
//         success: function (carreras) {
//             $("#tbody-carreras").empty();
//             $.each(carreras, function (Index, carrera) {
//                 //VARIABLES PARA DEFINIR BOTONES Y ESTETICA
//                 if (UserIsInRole("Administrador")) {
//                     let BotonDesahabilitar = '';
//                     let botones = '<button type="button" onclick="BuscarCarrera(' + carrera.carreraID + ')" class="btn btn-primary btn-sm" style="margin-right:5px" onkeyup="this.value = this.value.toUpperCase()">Editar</button>' +
//                         '<button type="button" onclick="DesahabilitarCarrera(' + carrera.carreraID + ',1)" class="btn btn-danger btn-sm">Desahabilitar</button>';
//                     //DEFINE SI ESTA ELIMINADA
//                     if (carrera.eliminar) {
//                         BotonDesahabilitar = 'table-danger';
//                         botones = '<button type="button" onclick="DesahabilitarCarrera('
//                             + carrera.carreraID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
//                     }

//                     $("#tbody-carreras").append('<tr class=' + BotonDesahabilitar + '>'
//                         + '<td>' + carrera.nombreCarrera + '</td>'
//                         + '<td>' + carrera.duracion + ' AÑOS' + '</td>'
//                         + '<td class="text-center">' + botones + '</td>' + '</tr>');
//                 }
//             });
//         },

//         // código a ejecutar si la petición falla;
//         // son pasados como argumentos a la función
//         // el objeto de la petición en crudo y código de estatus de la petición
//         error: function (xhr, status) {
//             alert('Error al cargar carreras');
//         },

//         // código a ejecutar sin importar si la petición falló o no
//         complete: function (xhr, status) {
//             //alert('Petición realizada');
//         }
//     });
// }






function VaciarFormulario() {
    $("#NombreCarrera").val('');
    $("#Duracion").val('');
    $("#CarreraID").val(0);
}

function BuscarCarrera(CarreraID) {
    // $("#Titulo-Modal-Servicio").text("Editar Subcategoria");
    $("#CarreraID").val(CarreraID);
    $.ajax({
        type: 'GET',
        // el tipo de información que se espera de respuesta
        dataType: 'json',

        url: '../../Carreras/BuscarCarreras',
        data: { CarreraID: CarreraID },
        success: function (carreras) {
            // console.log(servicios);
            if (carreras.length == 1) {
                let carrera = carreras[0];
                // console.log(servicio);
            $("#NombreCarrera").val(carrera.nombreCarrera);
            $("#Duracion").val(carrera.duracion);
             $("#CarreraID").val(carrera.carreraID)
             $("#ModalCarrera").modal("show");
         }
        },
        error: function (data) {
        }
    });
}

function GuardarCarrera() {
    //JAVASCRIPT
    let nombreCarrera1 = $("#NombreCarrera").val();
    let duracion = $("#Duracion").val();
    let carreraID = $("#CarreraID").val();

    var nombrecarrera = document.getElementById("NombreCarrera").value;
    //Llamar a todos los input
    var Duracion = document.getElementById("Duracion").value;


    if (nombrecarrera.trim() === "") {
        alert("El campo 'Nombre Carrera' es obligatorio.");
        return;
    }
    if (Duracion.trim() === "") {
        alert("El campo 'Duracion' es obligatorio.");
        return;
    }
    $.ajax({
        // la URL para la petición
        url: '../../Carreras/GuardarCarrera',
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { CarreraID: carreraID, NombreCarrera: nombreCarrera1 , Duracion: duracion},
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                $("#ModalCarrera").modal("hide");
                BuscarCarreras();
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

// function DesahabilitarCarrera(CarreraID, Eliminar) {
//     $.ajax({
//         // la URL para la petición
//         url: '../../Carreras/DesahabilitarCarrera',
//         // la información a enviar
//         // (también es posible utilizar una cadena de datos)
//         data: { CarreraID: CarreraID, Eliminar: Eliminar },
//         // especifica si será una petición POST o GET
//         type: 'POST',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',
//         // código a ejecutar si la petición es satisfactoria;
//         // la respuesta es pasada como argumento a la función
//         success: function (resultado) {
//             if (resultado) {
//                 BuscarCarreras();
               
//             }
//         },

//         // código a ejecutar si la petición falla;
//         // son pasados como argumentos a la función
//         // el objeto de la petición en crudo y código de estatus de la petición
//         error: function (xhr, status) {
//             alert('Disculpe, existió un problema');
//         }
        
//     });

// }


function DesahabilitarCarrera(CarreraID, Eliminar) {
    $.ajax({
        // la URL para la petición
        url: '../../Carreras/DesahabilitarCarrera',
        // la información a enviar
        data: { CarreraID: CarreraID, Eliminar: Eliminar },
        // especifica si será una petición POST o GET
        type: 'POST',
        // el tipo de información que se espera de respuesta
        dataType: 'json',
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                // Actualizar la vista o realizar otras acciones si la carrera se deshabilita con éxito
                BuscarCarreras();
            } else {
                // Mostrar la alerta de error si no se puede deshabilitar la carrera
                alert("No se puede deshabilitar la carrera, hay asignaturas asociadas activas.");
            }
        },
        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            mostrarAlertaError('Disculpe, existió un problema');
        }
    });
}



