$(document).ready(function () {
    cargarTabla();
});

function cargarTabla() {
    var table = $('#table-usuarios').DataTable();
    table.destroy();
    $('#table-usuarios').DataTable({
        "autoWidth": true,
        "processing": true,
        "ajax": baseUrl + "Usuario/ObtenerTodos",
        "columns": [
            { "data": "Id", visible: false, searchable: false },
            { "data": "Nombre" },
            { "data": "Cuenta" },
            { "data": "Correo" },
            { "data": "Rol" }
        ],
        select: true
    });
}

function registrar() {
    var nombre = $.trim($('#nombre').val());
    var cuenta = $.trim($('#cuenta').val());
    var correo = $.trim($('#correo').val());
    var password = $.trim($('#password').val());
    var comprobacion = $.trim($('#comprobacion').val());
    var permiso = $.trim($('#permiso').val());

    var todoLleno = true;
    var pwdCorrecto = false;

    if (nombre === "") todoLleno = false;
    if (cuenta === "") todoLleno = false;
    if (correo === "") todoLleno = false;
    if (password === "") todoLleno = false;
    if (comprobacion === "") todoLleno = false;
    if (permiso === "") todoLleno = false;

    if (password === comprobacion) pwdCorrecto = true;

    if (todoLleno) {
        if (pwdCorrecto) {

            $.ajax({
                url: baseUrl + "Usuario/RegistrarUsuario",
                data: {
                    nombre: nombre, cuenta: cuenta, correo: correo, password: password, permiso: permiso
                },
                cache: false,
                traditional: true,
                success: function (data) {
                    if (data === "true"){
                        swal("Correcto", "Cuenta guardado exitosamente.", "success");
                        $('#modal-registro').modal("hide");
                        cargarTabla();
                    } else {
                        swal({
                            text: "Ocurrió un problema.",
                            icon: "error"
                        });
                    }
                }, 
                error: function (xhr, exception) {
                    swal({
                        text: "Ocurrió un problema.",
                        icon: "error"
                    });
                }
            });
            
        } else {
            swal({
                text: "La comprobación de la contraseña no coincide",
                icon: "error"
            });
        }
    } else {
        swal({
            text: "Es necesario llenar todos los campos",
            icon: "warning"
        });
    }

}

function nuevo() {
    $('#modal-registro').modal();
}


function edit() {
    var modalC = $("#modal-edit-registro-content");
    var id = obtenerId();

    console.log("id: " + id);

    if (id !== 0) {

        $('#modal-edit-registro').modal();
        modalC.load(baseUrl + 'Usuario/EditarRegistro/' + id, function () {
            data = cargarDatos();
        });


    } else {
        swal("Error", "Seleccione un registro", "warning");
        //$('#mdMain').modal();
        return false;
    }
    //$('#mdMain').modal();
    //modalC.load(baseUrl + 'Usuario/Edit', {});
}

function guardarCambios() {
    var id = $.trim($('#usuario-id').val());
    var nombre = $.trim($("#edit-nombre").val());
    var cuenta = $.trim($("#edit-cuenta").val());
    var correo = $.trim($("#edit-correo").val());
   // var contrasenaVieja = $.trim($('#edit-password-viejo').val());
    var contrasena = $.trim($('#edit-password').val());
    var comprobacionContrasena = $.trim($('#edit-comprobacion').val());
    var permiso = $.trim($("#edit-permiso").val());
    
    var todoLleno = true;
    var pwdCorrecto = false;

    if (id === "") todoLleno = false;
    if (nombre === "") todoLleno = false;
    if (cuenta === "") todoLleno = false;
    if (correo === "") todoLleno = false;
   // if (contrasenaVieja === "") todoLleno = false;
    if (contrasena === "") todoLleno = false;
    if (comprobacionContrasena === "") todoLleno = false;
    if (permiso === "") todoLleno = false;

    if (contrasena === comprobacionContrasena) pwdCorrecto = true;

    if (todoLleno) {
        if (pwdCorrecto) {
            $.ajax({
                url: baseUrl + "Usuario/GuardarCambios",
                data: { id: id , nombre: nombre, cuenta: cuenta, correo: correo, contrasena: contrasena, permiso: permiso},
                type: 'POST',
                traditional: true,
                cache: false,
                success: function (data) {
                    if (data === "true") {
                        swal({
                            title: "Cambios Guardados",
                            icon: "success"
                        });
                        $('#modal-edit-registro').modal("hide");
                        cargarTabla();
                    } else {
                        swal({
                            text: "Ocurrió un problema y no se pudo completar la acción.",
                            icon: "warning"
                        });
                    }
                },
                error: function (xhr, exception) {
                    swal({
                        text: "Ocurrió un problema y no se pudo completar la acción.",
                        icon: "warning"
                        });
                }
            });
        } else {
            swal({
                text: "Las contraseñas no coinciden.",
                icon: "error"
            });      
        }
    } else {
        swal({
            text: "Todos los campos deben ser llenados.",
            icon: "warning"
        });
    }

    
}

function comprobarContrasenaVieja(correo, contrasenaVieja) {
    var coincide;

    $.ajax({
        url: baseUrl + "Usuario/ComprobarPwdViejo",
        data: { correo: correo, contrasenaVieja: contrasenaVieja },
        type: "POST",
        traditional: true,
        cache: false,
        success: function (data) {
            if (data === "true") {

                coincide = true;
                
            } else {

                coincide = false;
                    
            }
        },
        error: function (xhr, exception) {

            coincide = false;

        }
    });

    console.log("coincide: " + coincide);
    return coincide;
}

function cargarDatos() {
    var id = $.trim($('#usuario-id').val());

    if (id !== "" && id !== 0) {
        $.ajax({
            url: baseUrl + "Usuario/ObtenerPorId",
            data: { id: id },
            type: 'GET',
            dataType: 'json',
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#edit-nombre").val(data.Nombre);
                $("#edit-cuenta").val(data.Cuenta);
                $("#edit-correo").val(data.Correo);
                $("#edit-permiso").val(data.Rol);

                console.log("Nombre: " + data.Nombre);
                console.log("Cueta: " + data.Cuenta);
                console.log("Correo: " + data.Cuenta);
            }
        });


    }

}

function eliminar() {
    var id = obtenerId();

    if (id !== 0) {
        swal("¿Esta seguro que desea eliminar el registro?", {
            buttons: {
                si: {
                    text: "¡Seguro!",
                    value: "true"
                },
                no: {
                    text: "No.",
                    value: "false"
                }
            },
            dangerMode: true
        })
            .then((value) => {
                switch (value) {

                    case "true":
                        $.ajax({
                            url: baseUrl + "Usuario/Delete/" + id,
                            data: { id: id },
                            cache: false,
                            traditional: true,
                            success: function (data) {
                                if (data === "true") {
                                    swal("Exito", "Registro Borrado", "success");
                                    cargarTabla();
                                } else {
                                    swal("Error", "Ocurrió un problema", "warning");
                                }
                            }
                        });
                        break;

                    case "false":

                        swal({
                            text: "Operación cancelada",
                            icon: "error"
                        });
                        break;

                    default:
                        swal({
                            text: "Operación cancelada",
                            icon: "error"
                        });
                }
            });

    } else {
        swal({
            text: "Seleccione un registro",
            icon: "warning"
        });
    }
}

function obtenerId() {

    var table = $('#table-usuarios').DataTable();
    var id = 0;
    if (table.$('.selected')[0] !== undefined) {
        console.log("so it was defined");

        var selectedIndex = table.$('.selected')[0]._DT_RowIndex; //should be .index();   ??
        console.log("Selected index: " + selectedIndex);
        var row = table.row(selectedIndex).data();
        id = row.Id;
    } else {
        console.log("the little shit is undefined");
    }
    return id;
}