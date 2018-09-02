(function () {

    function bootstrap_application() {
        console.log("Starging the fracture app!");
        document.body.innerHTML = "<div id='fracture-app'><fracture-app></fracture-app></div>'";
        var vue = new Vue({ el: '#fracture-app' });
    }

    document.addEventListener('DOMContentLoaded', bootstrap_application, false);
})();