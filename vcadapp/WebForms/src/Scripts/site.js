// Pequenas melhorias de UX com jQuery
$(function () {
    // Mensagens de sucesso desaparecem apÃ³s alguns segundos
    var $msg = $(".success-message");
    if ($msg.length && $msg.text().trim() !== "") {
        setTimeout(function () {
            $msg.fadeOut(500);
        }, 4500);
    }

    // Foco no primeiro campo
    var $firstInput = $(".card .input").first();
    if ($firstInput.length) {
        $firstInput.focus();
    }
});
