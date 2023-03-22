var resultContainer;
var lastResult, countResults = 0;

function loadScanner() {

    const html5QrCode = new Html5Qrcode("qr-reader");
    const qrCodeSuccessCallback = (decodedText, decodedResult) => {
        if (decodedText !== lastResult) {
            ++countResults;
            lastResult = decodedText;
            // Handle on success condition with the decoded message.
            document.getElementById("qr-reader-results").appendChild = "<option>" + decodedText + "</option>";

            console.log(document.getElementById("qr-reader-results"));

            console.log(`Scan result ${decodedText}`, decodedResult);
        };
    }
    const config = {
        fps: 10, qrbox: { height: 250, width: 250 }
    };

    // If you want to prefer back camera
    html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);

}