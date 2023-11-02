
window.onload = function () {
    BuscarProfesores();
    BuscarProfesoresAsignaturas();
};

function BuscarProfesores() {
    $("#tbody-profesor").empty();
    // $("#tbody-profesor-imprimir").empty();
    $.ajax({
        url: '../../Profesores/BuscarProfesores',
        data: {},
        type: 'GET',
        dataType: 'json',
        success: function (profesores) {
            $("#tbody-profesor").empty();
            $.each(profesores, function (Index, profesor) {
                let BotonDesahabilitar = '';
                let botones = '<button type="button" onclick="BuscarAsignatura(' + profesor.profesorID + ')" class="btn btn-primary boton2" data-bs-toggle="modal" data-bs-target="#ModalProfesorAsignatura">Asignatura</button> ' +
                    '<button type="button" onclick="BuscarProfesor(' + profesor.profesorID + ')" class="btn btn-primary btn-sm" style="margin-right:5px">Editar</button>' +
                    '<button type="button" onclick="DesahabilitarProfesor(' + profesor.profesorID + ',1)" class="btn btn-danger btn-sm">Desahabilitar</button>';
                if (profesor.eliminarProfesor) {
                    BotonDesahabilitar = 'table-danger';
                    botones = '<button type="button" onclick="DesahabilitarProfesor(' + profesor.profesorID + ',0)" class="btn btn-warning btn-sm">Activar</button>';
                }

                // Formatear la fecha al formato español
                let fechas = new Date(profesor.fechaNacimiento);
                let fechaFormatted = fechas.toLocaleDateString('es-ES');

                $("#tbody-profesor").append('<tr class=' + BotonDesahabilitar + '>' +
                    '<td>' + profesor.nombreCompletoProfesor + '</td>' +
                    '<td>' + profesor.dni + '</td>' +
                    '<td>' + fechaFormatted + '</td>' +
                    '<td>' + profesor.dirrecion + '</td>' +
                    '<td>' + profesor.correo + '</td>' +
                    '<td class="text-center">' + botones + '</td>' + '</tr>');
            });
        },
        error: function (xhr, status) {
            alert('Error al cargar profesores');
        }
    });
}


function BuscarProfesoresAsignaturas() {
    $("#tbody-profesorAsignatura").empty();
    let profesorAsignaturaID = $("#ProfesorModalID").val();

    $.ajax({
        url: '../../Profesores/BuscarProfesoresAsignaturas',
        data: { ProfesorAsignaturaID: profesorAsignaturaID },
        type: 'GET',
        dataType: 'json',
        success: function (profesoresAsignaturas) {
            $("#tbody-profesorAsignatura").empty();
            $.each(profesoresAsignaturas, function (Index, profesorAsignatura) {
                let boton = '<button type="button" onclick="EliminarAsignaturaprofesor(' + profesorAsignatura.profesorAsignaturaID + ')" class="btn btn-primary boton2">Eliminar</button> ';
                $("#tbody-profesorAsignatura").append('<tr>' +
                    '<td>' + profesorAsignatura.nombreAsignatura + '</td>' +
                    '<td class="text-center">' + boton + '</td>' +
                    '</tr>');
            });
        },
        error: function (xhr, status) {
            alert('Error al cargar profesoresAsignaturas');
        }
    });
}




function GuardarProfesorAsignatura() {
    let profesorAsignaturaID = $("#ProfesorAsignaturaID").val();

    let profesorID = $('#ProfesorModalID').val();
    let asignaturaID = $("#AsignaturaID").val();

    $.ajax({
        url: '../../Profesores/GuardarProfesorAsignatura',
        data: { ProfesorAsignaturaID: profesorAsignaturaID, ProfesorID: profesorID, AsignaturaID: asignaturaID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                $("#ModalProfesorAsignatura").modal("show");
                BuscarProfesoresAsignaturas();               
            } else {
                alert("Complete todos los campos");
            }
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}

function VaciarFormularioProfesorAsignatura() {
    $("#AsignaturaID").val(1);
}

function VaciarFormulario() {
    $("#ProfesorID").val(0);
    $("#NombreCompletoProfesor").val('');
    $("#DNI").val('');
    $("#FechaNacimiento").val('');
    $("#Dirrecion").val('');
    $("#Correo").val('');
}

function BuscarAsignatura(profesorID) {
    $("#ProfesorModalID").val(profesorID);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '../../Profesores/BuscarProfesores',
        data: { ProfesorID: profesorID },
        success: function (profesores) {
            if (profesores.length == 1) {
                let profesor = profesores[0];

                $("#ProfesorModalID").val(profesor.profesorID);
                BuscarProfesoresAsignaturas();
                $("#ModalProfesorAsignatura").modal("show");
            }
        },
        error: function (data) {
            // Manejo de errores
        }
    });
}


function BuscarProfesor(profesorID) {
    $("#ProfesorID").val(profesorID);
    $.ajax({
        type: 'GET',
        dataType: 'json',
        url: '../../Profesores/BuscarProfesores',
        data: { ProfesorID: profesorID },
        success: function (profesores) {
            if (profesores.length == 1) {
                let profesor = profesores[0];
                $("#NombreCompletoProfesor").val(profesor.nombreCompletoProfesor);
                $("#DNI").val(profesor.dni);

                // Formatea la fecha en formato "AAAA-MM-DD" para el campo input date
                let fechaNacimiento = new Date(profesor.fechaNacimiento);
                let fechaFormatted = fechaNacimiento.toISOString().split('T')[0];
                $("#FechaNacimiento").val(fechaFormatted);

                $("#Dirrecion").val(profesor.dirrecion);
                $("#Correo").val(profesor.correo);
                $("#ProfesorID").val(profesor.profesorID);
                $("#ModalProfesor").modal("show");
            }
        },
        error: function (data) {
            // Manejo de errores
        }
    });
}


function GuardarProfesor() {
    let nombreCompletoProfesor = $("#NombreCompletoProfesor").val();
    let dni = $("#DNI").val();
    let fechaNacimiento = $("#FechaNacimiento").val();
    let dirrecion = $("#Dirrecion").val();
    let correo = $("#Correo").val();
    let ProfesorID = $("#ProfesorID").val();

    var nombreCompleto = document.getElementById("NombreCompletoProfesor").value;
    //Llamar a todos los input
    var dNI = document.getElementById("DNI").value;

    var FechaNacimiento = document.getElementById("FechaNacimiento").value;

    var dirRecion = document.getElementById("Dirrecion").value;

    var CoRreo = document.getElementById("Correo").value;
    // Obtén los valores de los otros campos aquí

    if (nombreCompleto.trim() === "") {
        alert("El campo 'Nombre Completo Profesor' es obligatorio.");
        return;
    }
    if (dNI.trim() === "") {
        alert("El campo 'DNI' es obligatorio.");
        return;
    }
    if (FechaNacimiento.trim() === "") {
        alert("El campo 'Fecha Nacimiento' es obligatorio.");
        return;
    }
    if (dirRecion.trim() === "") {
        alert("El campo 'Dirrecion' es obligatorio.");
        return;
    }
    if (CoRreo.trim() === "") {
        alert("El campo 'Correo' es obligatorio.");
        return;
    }

    $.ajax({
        url: '../../Profesores/GuardarProfesor',
        data: { ProfesorID: ProfesorID, NombreCompletoProfesor: nombreCompletoProfesor, DNI: dni, FechaNacimiento: fechaNacimiento, Dirrecion: dirrecion, Correo: correo },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                $("#ModalProfesor").modal("hide");
                BuscarProfesores();
            }
            else {
                alert("Complete todos los campos");
            }
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}

function DesahabilitarProfesor(profesorID, eliminarProfesor) {
    $.ajax({
        url: '../../Profesores/DesahabilitarProfesor',
        data: { ProfesorID: profesorID, EliminarProfesor: eliminarProfesor },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                BuscarProfesores();
            }
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}

function EliminarAsignaturaprofesor(ProfesorAsignaturaID) {
    $.ajax({
        url: '../../Profesores/EliminarAsignaturaprofesor',
        data: { ProfesorAsignaturaID: ProfesorAsignaturaID },
        type: 'POST',
        dataType: 'json',
        success: function (resultado) {
            if (resultado) {
                // Llamada exitosa, puedes realizar alguna acción adicional si es necesario
                $("#ModalProfesorAsignatura").modal("hide");
                BuscarProfesoresAsignaturas();
                VaciarFormularioProfesorAsignatura();
            } else {
                alert("Hubo un problema al eliminar la asignatuara.");
            }
        },
        error: function (xhr, status) {
            alert('Disculpe, existió un problema');
        }
    });
}



