var telefonoItems = new Listas();
var emailItems = new Listas();
var direccionesItems = new Listas();


// Mostrar todos los telefonos de la lista
function MostrarTelefonos(editar) {
    $("#telItems").html('');
    if (telefonoItems.Total() > 0) {
        var $table = $('<table class="table table-bordered table-striped table-responsive"/>');
        if (editar) {
            $table.append('<thead><tr><th>Telefono</th><th>Tipo</th><th>Principal</th><th>Opciones</th></tr></thead>');
        } else {
            $table.append('<thead><tr><th>Telefono</th><th>Tipo</th><th>Principal</th></tr></thead>')
        }
        var $tbody = $('<tbody/>');
        var textPrincipal = 'No';
        for (var i = 0; i < telefonoItems.Total(); i++) {
            var $row = $('<tr/>');
            if (telefonoItems.Item(i).Principal) {
                textPrincipal = 'Si';
            } else {
                textPrincipal = 'No';
            }
            $row.append($('<td/>').html(telefonoItems.Item(i).Numero));
            $row.append($('<td/>').html(telefonoItems.Item(i).Tipo));
            $row.append($('<td/>').html(textPrincipal));
            if (editar) {
                $row.append($('<td/>').html('<a href="#" class="btn btn-danger" data-toggle="tooltip" title="eliminar" onClick="return EliminarTel(' + i + ');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>'));
            }
            $tbody.append($row);
        }

        $table.append($tbody);
        $("#telItems").html($table);
    }
}

// Mostrar todos los emails de la lista
function MostrarEmails(editar) {
    $("#emailItems").html('');
    if (emailItems.Total() > 0) {
        var $table = $('<table class="table table-bordered table-striped table-responsive"/>');
        if (editar) {
            $table.append('<thead><tr><th>Email</th><th>Principal</th><th>Opciones</th></tr></thead>');
        } else {
            $table.append('<thead><tr><th>Email</th><th>Principal</th></tr></thead>')
        }
        var $tbody = $('<tbody/>');
        var textPrincipal = 'No';
        for (var i = 0; i < emailItems.Total(); i++) {
            var $row = $('<tr/>');
            if (emailItems.Item(i).Principal) {
                textPrincipal = 'Si';
            } else {
                textPrincipal = 'No';
            }
            $row.append($('<td/>').html(emailItems.Item(i).Direccion));
            $row.append($('<td/>').html(textPrincipal));
            if (editar) {
                $row.append($('<td/>').html('<a href="#" class="btn btn-danger" data-toggle="tooltip" title="eliminar" onClick="return EliminarEmail(' + i + ');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>'));
            }
            $tbody.append($row);
        }

        $table.append($tbody);
        $("#emailItems").html($table);
    }
}


// Mostrar todas las direcciones de la lista
function MostrarDirecciones(editar) {
    $("#dirItems").html('');
    if (direccionesItems.Total() > 0) {
        var $table = $('<table class="table table-bordered table-striped table-responsive"/>');
        if (editar) {
            $table.append(`
                <thead>
                    <tr>
                        <th>Calle</th>
                        <th>Numero Exterior</th>
                        <th>Número Interior</th>
                        <th>Departamento</th>
                        <th>Municipio</th>
                        <th>Principal</th>
                        <th>Opciones</th>
                    </tr>
                </thead>
            `);
        } else {
            $table.append(`
                <thead>
                    <tr>
                        <th>Calle</th>
                        <th>Numero Exterior</th>
                        <th>Número Interior</th>
                        <th>Departamento</th>
                        <th>Municipio</th>
                        <th>Principal</th>
                    </tr>
                </thead>
            `);
        }
        var $tbody = $('<tbody/>');
        var textPrincipal = 'No';
        for (var i = 0; i < direccionesItems.Total(); i++) {
            var $row = $('<tr/>');
            if (direccionesItems.Item(i).Principal) {
                textPrincipal = 'Si';
            } else {
                textPrincipal = 'No';
            }
            $row.append($('<td/>').html(direccionesItems.Item(i).Calle));
            $row.append($('<td/>').html(direccionesItems.Item(i).NumExterior));
            $row.append($('<td/>').html(direccionesItems.Item(i).NumInterior));
            $row.append($('<td/>').html(direccionesItems.Item(i).Departamento));
            $row.append($('<td/>').html(direccionesItems.Item(i).Municipio));
            $row.append($('<td/>').html(textPrincipal));
            if (editar) {
                $row.append($('<td/>').html('<a href="#" class="btn btn-danger" data-toggle="tooltip" title="eliminar" onClick="return EliminarDir(' + i + ');"><span class="glyphicon glyphicon-trash" aria-hidden="true"></span>'));
            }
            $tbody.append($row);
        }

        $table.append($tbody);
        $("#dirItems").html($table);
    }
}


// Agregar un telefono a la lista
function AddTelefono_Click() {
    var isValidItem = true;
    if ($("#Telefono").val().trim() == '') {
        isValidItem = false;
        $("#Telefono").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#Telefono").siblings('span.error').css('visibility', 'hidden');
    }

    if ($("#Tipo").val().trim() == '') {
        isValidItem = false;
        $("#Tipo").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#Tipo").siblings('span.error').css('visibility', 'hidden');
    }

    if (isValidItem) {
        var vPrincipal;
        if ($("#TelPrincipal").is(':checked')) {
            vPrincipal = true;
        } else {
            vPrincipal = false;
        }

        telefonoItems.Agregar({
            TelefonoId: 0,
            Numero: $("#Telefono").val().trim(),
            Tipo: $("#Tipo").val().trim(),
            Principal: vPrincipal,
        });

        $("#Telefono").val('').focus();
        $("#Tipo").val('');
        $("#TelPrincipal").attr('checked', false);

    }

    

    MostrarTelefonos(true);
}


// Agregar un email a la lista
function AddEmail_Click() {
    var isValidItem = true;
    if ($("#Email").val().trim() == '') {
        isValidItem = false;
        $("#Email").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#Email").siblings('span.error').css('visibility', 'hidden');
    }

    if (isValidItem) {
        var vPrincipal;
        if ($("#EmailPrincipal").is(':checked')) {
            vPrincipal = true;
        } else {
            vPrincipal = false;
        }

        emailItems.Agregar({
            EmailId: 0,
            Direccion: $("#Email").val().trim(),
            Principal: vPrincipal,
        });

        $("#Email").val('').focus();
        $("#EmailPrincipal").attr('checked', false);

    }

    MostrarEmails(true);
}


// Agregar una direccion a la lista
function AddDireccion_Click() {
    var isValidItem = true;
    if ($("#Calle").val().trim() == '') {
        isValidItem = false;
        $("#Calle").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#Calle").siblings('span.error').css('visibility', 'hidden');
    }

    if ($("#NumExterior").val().trim() == '') {
        isValidItem = false;
        $("#NumExterior").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#NumExterior").siblings('span.error').css('visibility', 'hidden');
    }

    if ($("#Departamento").val().trim() == '') {
        isValidItem = false;
        $("#Departamento").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#Departamento").siblings('span.error').css('visibility', 'hidden');
    }

    if ($("#Municipio").val().trim() == '') {
        isValidItem = false;
        $("#Municipio").siblings('span.error').css('visibility', 'visible');
    } else {
        $("#Municipio").siblings('span.error').css('visibility', 'hidden');
    }

    if (isValidItem) {
        var vPrincipal;
        if ($("#DirPrincipal").is(':checked')) {
            vPrincipal = true;
        } else {
            vPrincipal = false;
        }

        direccionesItems.Agregar({
            DireccionId: 0,
            Calle: $("#Calle").val().trim(),
            NumExterior: $("#NumExterior").val().trim(),
            NumInterior: $("#NumInterior").val().trim(),
            Departamento: $("#Departamento").val().trim(),
            Municipio: $("#Municipio").val().trim(),
            Principal: vPrincipal,
        });

        $("#Calle").val('').focus();
        $("#NumExterior").val('');
        $("#NumInterior").val('');
        $("#Departamento").val('');
        $("#Municipio").val('');
        $("#DirPrincipal").attr('checked', false);

    }
    MostrarDirecciones(true);
}


// Eliminar un telefono de la lista
function EliminarTel(indice) {
    telefonoItems.Eliminar(indice);
    MostrarTelefonos(true);
    return false;
}

// Eliminar un email de la lista
function EliminarEmail(indice) {
    emailItems.Eliminar(indice);
    MostrarEmails(true);
    return false;
}

// Eliminar un telefono de la lista
function EliminarDir(indice) {
    direccionesItems.Eliminar(indice);
    MostrarDirecciones(true);
    return false;
}


// Guarda los datos del clientes en la BD

function crear_CLick() {
    // Valdacion dell cliente
    var isAllValid = true;
    if ($("#Nombre").val().trim() == '') {
        $("#Nombre").siblings('span.error').css('visibility', 'visible');
        isAllValid = false;
    } else {
        $("#Nombre").siblings('span.error').css('visibility', 'hidden');
    }

    if ($("#TipoClienteId").val().trim() == '') {
        $("#TipoClienteId").siblings('span.error').css('visibility', 'visible');
        isAllValid = false;
    } else {
        $("#TipoClienteId").siblings('span.error').css('visibility', 'hidden');
    }

    var data = {
        ClienteId: 0,
        Nombre: $("#Nombre").val().trim(),
        RFC: $("#RFC").val().trim(),
        TipoClienteId: $("#TipoClienteId").val().trim(),
        TipoPersonaSat: $("#TipoPersonaSat").val().trim(),
        Telefonos: telefonoItems.lista,
        Correos: emailItems.lista,
        Direcciones: direccionesItems.lista
    };

    var token = $('[name=__RequestVerificationToken]').val();

    $.ajax({
        url: '/Clientes/Create',
        type: 'POST',
        data: { __RequestVerificationToken: token, cliente: data },
        success: ((d) => {
            if (d == true) {
                window.location.href = '/Clientes/Index';
            } else {
                alert('Error al guardar los datos del cliente');
            }
        }),
        error: ((e) => {
            console.log(e);
            alert("Error, intente nuevamente");
        })
    });
}


// Guardar las modificaciones de los datos del cliente
function modificar_CLick() {
    // Validación del cliente
    var isAllValid = true;

    if ($('#Nombre').val().trim() == '') {
        $('#Nombre').siblings('span.error').css('visibility', 'visible');
    } else {
        $('#Nombre').siblings('span.error').css('visibility', 'hidden');
    }

    if ($('#TipoClienteId').val().trim() == '') {
        $('#TipoClienteId').siblings('span.error').css('visibility', 'visible');
    } else {
        $('#TipoClienteId').siblings('span.error').css('visibility', 'hidden');
    }

    if (isAllValid) {
        var data = {
            ClienteId: $('#ClienteId').val().trim(),
            Nombre: $('#Nombre').val().trim(),
            RFC: $('#RFC').val().trim(),
            TipoPersonaSat: $('#TipoPersonaSat').val().trim(),
            TipoClienteId: $('#TipoClienteId').val().trim(),
            Telefonos: telefonoItems.lista,
            Correos: emailItems.lista,
            Direcciones: direccionesItems.lista
        }
        var idCliente = $('#ClienteId').val().trim();
        var token = $('[name=__RequestVerificationToken]').val();
        var url = '/Clientes/Edit/' + idCliente
        $.ajax({
            url: url,
            type: "POST",
            data: { __RequestVerificationToken: token, cliente: data },
            success: ((d) => {
                if (d === true) {
                    window.location.href = "/Clientes/Index";
                } else {
                    alert('Ocurrio un error al intentar guardar los cambios');
                }
            }),
            error: (() => {
                alert('Error, porfavor intente de nuevo');
            })
        });
    }
}

