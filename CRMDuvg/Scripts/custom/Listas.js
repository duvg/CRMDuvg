function Listas() {
    this.lista = [];
    var self = this;


    this.Agregar = function (valor) {
        if ( self.lista.length > 0 ) {
            this.ModificarPrincipal(valor.Principal);
        } else {
            valor.Principal = true;
        }
        self.lista.push(valor);
    };

    this.Eliminar = funcrion(valor) {
        if (indice < self.lista.length) {
            self.lista.splice(indice, 1);
        } 
        this.ModificarPrincipal(false);
    }

    this.ModificarPrincipal = function (valor) {
        if (valor) {
            for (var i = 0; i < self.lista.length; i++) {
                self.lista[i].Principal = false;
            }
        } else {
            if (self.lista.length > 0) {

            }
        }
    }
}