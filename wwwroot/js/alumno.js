// window.onload = BuscarAlumnos();

// function BuscarAlumnos() {
//     $("#tbody-alumnos").empty();
//     $.ajax({
//         // la URL para la petición
//         url: '../../Alumnos/BuscarAlumno',
//         // la información a enviar
//         // (también es posible utilizar una cadena de datos)
//         data: {},
//         // especifica si será una petición POST o GET
//         type: 'GET',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',
//         // código a ejecutar si la petición es satisfactoria;
//         // la respuesta es pasada como argumento a la función
//         success: function (alumnos) {
//             $("#tbody-alumnos").empty();
//             $.each(alumnos, function (index, alumno) {
//                 let BotonDesahabilitar = '';
//                 let botones = '<button type="button" onclick="BuscarAlumno(' + alumno.AlumnoID + ')" class="btn btn-primary btn-sm" style="margin-right:5px">Editar</button>' +
//                     '<button type="button" onclick="DesahabilitarAlumno(' + alumno.AlumnoID + ',1)" class="btn btn-danger btn-sm">Desahabilitar</button>';
//                 if (alumno.Eliminar) {
//                     BotonDesahabilitar = 'table-danger';
//                     botones = '<button type="button" onclick="DesahabilitarAlumno(' + alumno.AlumnoID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
//                 }

//                 let fecha = new Date(alumno.Fecha); // Convertir la fecha a objeto Date
//                 let fechaFormatted = fecha.toLocaleDateString('es-ES'); // Formatear la fecha como "DD-MM-AAAA"

//                 $("#tbody-alumnos").append('<tr class="' + BotonDesahabilitar + '">' +
//                     '<td>' + alumno.NombreCompleto + '</td>' +
//                     '<td>' + fechaFormatted + '</td>' + // Mostrar la fecha formateada
//                     '<td>' + alumno.NombreCarrera + '</td>' +
//                     '<td class="text-center">' + botones + '</td>' + '</tr>');
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

// function VaciarFormulario() {
//     $("#NombreCompleto").val('');
//     $("#Fecha").val('');
//     $("#AlumnoID").val(0);
//     $("#carreraNombre").val(0);
// }

// function BuscarAlumno(AlumnoID) {
//     // $("#Titulo-Modal-Servicio").text("Editar Subcategoria");
//     $("#AlumnoID").val(AlumnoID);
//     $.ajax({
//         type: 'GET',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',

//         url: '../../Alumnos/BuscarAlumnos',
//         data: { AlumnoID: AlumnoID },
//         success: function (alumnos) {
//             // console.log(servicios);
//             if (alumnos.length == 1) {
//                 let alumno = alumnos[0];
//                 // console.log(servicio);
//                 $("#NombreCompleto").val(alumno.NombreCompelto);
//                 $("#Fecha").val(alumno.Fecha);
//                 $("#AlumnoID").val(alumno.AlumnoID)
//                 $("#carreraNombre").val(alumno.NombreCarrera)
//                 $("#ModalAlumno").modal("show");
//             }
//         },
//         error: function (data) {
//         }
//     });
// }

// function GuardarAlumno() {
//     //JAVASCRIPT
//     let nombreCompleto1 = $("#NombreCarrera").val();
//     let fecha = $("#Duracion").val();
//     let alumnoID = $("#AlumnoID").val();
//     let carreraID = $("#carreraNombre").val();
//     $.ajax({
//         // la URL para la petición
//         url: '../../Alumnos/GuardarAlumno',
//         // la información a enviar
//         // (también es posible utilizar una cadena de datos)
//         data: { AlumnoID: alumnoID, NombreCompelto: nombreCompleto1, Fecha: fecha, CarreraID: carreraID },
//         // especifica si será una petición POST o GET
//         type: 'POST',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',
//         // código a ejecutar si la petición es satisfactoria;
//         // la respuesta es pasada como argumento a la función
//         success: function (resultado) {
//             if (resultado) {
//                 $("#ModalAlumno").modal("hide");
//                 BuscarCarreras();
//             }
//             else {
//                 alert("Existe una Carrera con el mismo Nombre.");
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

// function Desahabilitaralumno(alumnoID, eliminar) {
//     $.ajax({
//         // la URL para la petición
//         url: '../../Alumnos/DesahabilitarAlumno',
//         // la información a enviar
//         // (también es posible utilizar una cadena de datos)
//         data: { AlumnoID: alumnoID, Eliminar: eliminar },
//         // especifica si será una petición POST o GET
//         type: 'POST',
//         // el tipo de información que se espera de respuesta
//         dataType: 'json',
//         // código a ejecutar si la petición es satisfactoria;
//         // la respuesta es pasada como argumento a la función
//         success: function (resultado) {
//             if (resultado) {
//                 BuscarAlumnos();

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

window.onload = function () {
    BuscarAlumnos();
};

function BuscarAlumnos() {
    $("#tbody-alumno").empty();
    $.ajax({
        url: '../../Alumnos/BuscarAlumnos',
        data: {},
        type: 'GET',
        dataType: 'json',
        success: function (alumnos) {
            $("#tbody-alumno").empty();
            $.each(alumnos, function (Index, alumno) {
                let BotonDesahabilitar = '';
                let botones = '<button type="button" onclick="BuscarAlumno(' + alumno.alumnoID + ')" class="btn btn-primary btn-sm" style="margin-right:5px">Editar</button>' +
                    '<button type="button" onclick="DesahabilitarAlumno(' + alumno.alumnoID + ',1)" class="btn btn-danger btn-sm">Desahabilitar</button>';
                if (alumno.eliminar) {
                    BotonDesahabilitar = 'table-danger';
                    botones = '<button type="button" onclick="DesahabilitarAlumno(' + alumno.alumnoID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                }

                // * ESTO SIRVE PARA TRANFORMAR LA FECHA AL FORMATO ESPAÑOL

                let fechas = new Date(alumno.fecha);
                let fechaFormatted = fechas.toLocaleDateString('es-ES');

                $("#tbody-alumno").append('<tr class=' + BotonDesahabilitar + '>' +
                    '<td>' + alumno.nombreCompleto + '</td>' +
                    '<td>' + alumno.dirrecionEstudiante + '</td>' +
                    '<td>' + fechaFormatted + '</td>' +
                    '<td>' + alumno.nombreCarrera + '</td>' +
                    '<td>' + alumno.dniEstudiante + '</td>' +
                    '<td>' + alumno.correoEstudiante + '</td>' +
                    '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },
        error: function (xhr, status) {
            alert('Error al cargar alumnos');
        }
    });
}

// function VaciarFormulario() {
//     $("#NombreCompleto").val('');
//     $("#Fecha").val('');
//     $("#AlumnoID").val(0);
//     $("#CarreraID").val(1);
// }
function VaciarFormulario() {
    $("#NombreCompleto").val('');
    $("#Fecha").val('');
    $("#AlumnoID").val(0);
    $("#CarreraID").val('');
    $("#DirrecionEstudiante").val('');
    $("#CorreoEstudiante").val('');
    $("#DNIEstudiante").val('');
}


function BuscarAlumno(AlumnoID) {
    $("#AlumnoID").val(AlumnoID);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '../../Alumnos/BuscarAlumnos',
        data: { AlumnoID: AlumnoID },
        success: function (alumnos) {
            if (alumnos.length == 1) {
                let alumno = alumnos[0];
                $("#NombreCompleto").val(alumno.nombreCompleto);
                $("#DNIEstudiante").val(alumno.dniEstudiante);


                // Formatea la fecha en formato "AAAA-MM-DD" para el campo input date
                let fecha = new Date(alumno.fecha);
                let fechaFormatted = fecha.toISOString().split('T')[0];
                $("#Fecha").val(fechaFormatted);

                // $("#Fecha").val(fechaFormatted);
                $("#AlumnoID").val(alumno.alumnoID);
                $("#CarreraID").val(alumno.carreraID);
                $("#DirrecionEstudiante").val(alumno.dirrecionEstudiante);
                $("#CorreoEstudiante").val(alumno.correoEstudiante);
                $("#ModalAlumno").modal("show");
            }
        },
        error: function (data) {
        }
    });
}




function GuardarAlumno() {
    let nombreCompleto = $("#NombreCompleto").val();
    let dniEstudiante = $("#DNIEstudiante").val();
    let fecha = $("#Fecha").val();
    let alumnoID = $("#AlumnoID").val();
    let carreraID = $("#CarreraID").val();
    let dirrecionEstudiante = $("#DirrecionEstudiante").val();
    let correoEstudiante = $("#CorreoEstudiante").val();

    var nombrecompleto = document.getElementById("NombreCompleto").value;
    //Llamar a todos los input
    var Fecha = document.getElementById("Fecha").value;

    var Carrera = document.getElementById("CarreraID").value;

    if (nombrecompleto.trim() === "") {
        alert("El campo 'Nombre Completo' es obligatorio.");
        return;
    }
    if (Fecha.trim() === "") {
        alert("El campo 'Fecha' es obligatorio.");
        return;
    }

    if (Carrera.trim() === "") {
        alert("El campo 'Carrera' es obligatorio.");
        return;
    }


    $.ajax({
        url: '../../Alumnos/GuardarAlumno',
        data: { AlumnoID: alumnoID, NombreCompleto: nombreCompleto, Fecha: fecha, CarreraID: carreraID, DirrecionEstudiante: dirrecionEstudiante, CorreoEstudiante: correoEstudiante, DNIEstudiante: dniEstudiante },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                $("#ModalAlumno").modal("hide");
                BuscarAlumnos();
            }
            else {
                alert("Existe un Alumno con el mismo Nombre.");
            }
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}

function DesahabilitarAlumno(alumnoID, eliminar) {
    $.ajax({
        url: '../../Alumnos/DesahabilitarAlumno',
        data: { AlumnoID: alumnoID, Eliminar: eliminar },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                BuscarAlumnos();
            }
            else
            {
                // Mostrar la alerta de error si no se puede deshabilitar la carrera
                window.alert("No se puede deshabilitar la carrera, hay asignaturas asociadas activas.");
            }
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}









