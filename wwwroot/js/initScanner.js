var resultContainer;
var lastResult, countResults = 0;

function loadScanner() {

    resultContainer = document.getElementById('qr-reader-results');

    const html5QrCode = new Html5Qrcode("qr-reader");
    const qrCodeSuccessCallback = (decodedText, decodedResult) => {
        if (decodedText !== lastResult) {
            ++countResults;
            lastResult = decodedText;
            // Handle on success condition with the decoded message.
            resultContainer.innerHtml = decodedResult;
            console.log(`Scan result ${decodedText}`, decodedResult);
        };
    }
    const config = { fps: 10, qrbox: 250 };

    // If you want to prefer back camera
    html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);

}