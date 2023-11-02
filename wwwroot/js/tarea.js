window.onload = BuscarTareas();

function BuscarTareas() {
    $("#tbody-tareas").empty();
    $.ajax({
        // la URL para la petición
        url: "../../Tareas/BuscarTareas",
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {},
        // especifica si será una petición POST o GET
        type: "GET",
        // el tipo de información que se espera de respuesta
        dataType: "json",
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (tareas) {
            $("#tbody-tareas").empty();
            $.each(tareas, function (Index, tarea) {
                //VARIABLES PARA DEFINIR BOTONES Y ESTETICA
                let BotonDesahabilitar = "";
                let botones =
                    '<button type="button" onclick="BuscarTarea(' +
                    tarea.tareaID +
                    ')" class="btn btn-primary btn-sm" style="margin-right:5px" onkeyup="this.value = this.value.toUpperCase()">Editar</button>' +
                    '<button type="button" onclick="DesahabilitarTarea(' +
                    tarea.tareaID +
                    ',1)" class="btn btn-danger btn-sm">Realizada</button>';
                //DEFINE SI ESTA ELIMINADA
                if (tarea.realizada) {
                    BotonDesahabilitar = "table-danger";
                    botones =
                        '<button type="button" onclick="DesahabilitarTarea(' +
                        tarea.tareaID +
                        ',0)" class="btn btn-warning btn-sm">No realizada</button>' +
                        '<button type="button" onclick="EliminarTarea(' +
                        tarea.tareaID +
                        ')" class="btn btn-warning btn-sm">Eliminar</button>';
                }

                // Asegurarse de que tarea.fechaCarga y tarea.fechaVencimiento sean fechas válidas
                const fechaCarga = tarea.fechaCarga ? new Date(tarea.fechaCarga) : null;
                const fechaVencimiento = tarea.fechaVencimiento
                    ? new Date(tarea.fechaVencimiento)
                    : null;

                // Función para formatear una fecha en el formato 'dd/mm/yyyy'
                function formatearFecha(fecha) {
                    if (fecha instanceof Date && !isNaN(fecha)) {
                        const opciones = {
                            year: "numeric",
                            month: "2-digit",
                            day: "2-digit",
                        };
                        return fecha.toLocaleDateString("es-ES", opciones);
                    }
                    return ""; // Devuelve una cadena vacía si la fecha no es válida o está ausente
                }

                // Formatear las fechas
                const fechaCargaFormateada = formatearFecha(fechaCarga);
                const fechaVencimientoFormateada = formatearFecha(fechaVencimiento);

                $("#tbody-tareas").append(
                    "<tr class=" +
                    BotonDesahabilitar +
                    ">" +
                    "<td>" +
                    tarea.titulo +
                    "</td>" +
                    "<td>" +
                    tarea.descripcion +
                    "</td>" +
                    '<td class="text-center">' +
                    fechaCargaFormateada +
                    "</td>" +
                    '<td class="text-center">' +
                    fechaVencimientoFormateada +
                    "</td>" +
                    "<td>" +
                    tarea.nombreAsignatura +
                    "</td>" +
                    // + '<td>' + tarea.nombreCompletoProfesor + '</td>'
                    '<td class="text-center">' +
                    botones +
                    "</td>" +
                    "</tr>"
                );
            });
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert("Error al cargar tareas");
        },

        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('Petición realizada');
        },
    });
}

function VaciarFormulario() {
    $("#Titulo").val("");
    $("#Descripcion").val("");
    $("#TareaID").val(0);
    $("#FechaCarga").val();
    $("#FechaVencimiento").val();
    $("#AsignaturaID").val("");
    // $("#ProfesorID").val('');
}

function BuscarTarea(tareaID) {
    $.ajax({
        // la URL para la petición
        url: "../../Tareas/BuscarTareas",
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { TareaID: tareaID },
        // especifica si será una petición POST o GET
        type: "GET",
        // el tipo de información que se espera de respuesta
        dataType: "json",
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (tareas) {
            if (tareas.length == 1) {
                let tarea = tareas[0];

                // Creamos una nueva instancia de Date usando la fecha de carga de la tarea
                let fechaCarga = new Date(tarea.fechaCarga);

                // Formateamos la fecha como una cadena en formato ISO (AAAA-MM-DD)
                let fechaFormatted = fechaCarga.toISOString().split("T")[0];

                // Establecemos el valor del campo de entrada con la fecha formateada
                $("#FechaCarga").val(fechaFormatted);

                // Hacemos lo mismo para la fecha de vencimiento
                let fechaVencimiento = new Date(tarea.fechaVencimiento);
                let fechaFormatted2 = fechaVencimiento.toISOString().split("T")[0];
                $("#FechaVencimiento").val(fechaFormatted2);

                $("#Titulo").val(tarea.titulo);

                $("#Descripcion").val(tarea.descripcion);
                $("#TareaID").val(tarea.tareaID);

                $("#AsignacionID").val(tarea.AsignacionID);

                // $("#ProfesorID").val(tarea.ProfesorID);

                $("#ModalTarea").modal("show");
            }
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert("Error al cargar tareas", error);
            console.error("Error al cargar tareas", error);
        },

        // código a ejecutar sin importar si la petición falló o no
        complete: function (xhr, status) {
            //alert('Petición realizada');
        },
    });
}

function GuardarTarea() {
    //JAVASCRIPT
    let titulo = $("#Titulo").val();
    let descripcion1 = $("#Descripcion").val();
    let tareaID = $("#TareaID").val();
    let prioridad = $("#Prioridad").val();
    let fechaCarga = $("#FechaCarga").val();
    let fechaVencimiento = $("#FechaVencimiento").val();
    let asignaturaID = $("#AsignaturaID").val();
    // let profesorID = $("#AsignaturaID").val();
    $.ajax({
        // la URL para la petición
        url: "../../Tareas/GuardarTarea",
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: {
            TareaID: tareaID,
            AsignaturaID: asignaturaID,
            Titulo: titulo,
            Descripcion: descripcion1,
            Prioridad: prioridad,
            FechaCarga: fechaCarga,
            FechaVencimiento: fechaVencimiento,
        },
        // especifica si será una petición POST o GET
        type: "POST",
        // el tipo de información que se espera de respuesta
        dataType: "json",
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                $("#ModalTarea").modal("hide");
                BuscarTareas();
            } else {
                alert("Existe una Tarea con la misma descripción.");
            }
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        error: function (xhr, status) {
            alert("Disculpe, existió un problema");
        },
    });
}

function DesahabilitarTarea(TareaID, Realizada) {
    //JAVASCRIPT
    $.ajax({
        // la URL para la petición
        url: "../../Tareas/DeshabilitarTarea",
        // la información a enviar
        // (también es posible utilizar una cadena de datos)
        data: { TareaID: TareaID, Realizada: Realizada },
        // especifica si será una petición POST o GET
        type: "POST",
        // el tipo de información que se espera de respuesta
        dataType: "json",
        // código a ejecutar si la petición es satisfactoria;
        // la respuesta es pasada como argumento a la función
        success: function (resultado) {
            if (resultado) {
                BuscarTareas();
            } else {
                alert("Existe una Categoría con la misma descripción.");
            }
        },

        // código a ejecutar si la petición falla;
        // son pasados como argumentos a la función
        // el objeto de la petición en crudo y código de estatus de la petición
        // error: function (xhr, status) {
        //     alert('Disculpe, existió un problema');
        // }
        error: function (xhr, status) {
            if (status === "error") {
                alert("Error: " + xhr.status + " " + xhr.statusText);
            } else {
                alert("Disculpe, existió un problema");
            }
        },
    });
}

function EliminarTarea(tareaID) {
    $.ajax({
        url: "../../Tareas/EliminarTarea",
        data: { TareaID: tareaID },
        type: "POST",
        dataType: "json",
        success: function (resultado) {
            if (resultado) {
                // Llamada exitosa, puedes realizar alguna acción adicional si es necesario
                BuscarTareas();
            } else {
                alert("Hubo un problema al eliminar la tarea.");
            }
        },
        error: function (xhr, status) {
            alert("Disculpe, existió un problema");
        },
    });
}
