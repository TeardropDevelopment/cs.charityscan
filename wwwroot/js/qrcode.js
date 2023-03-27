let html5QrCode;
let oldScanTime = new Date().getTime();

// Proxy function
// blazorInstance: A reference to the actual C# class instance, required to invoke C# methods inside it
// blazorCallbackName: parameter that will get the name of the C# method used as callback
window.scannerProxy = (instance, callbackMethod) => {
    // Execute function that will do the actual job
    window.start(result => {
        // Invoke the C# callback method passing the result as parameter
        instance.invokeMethodAsync(callbackMethod, result);
    });
}

function start(callBack) {
    html5QrCode = new Html5Qrcode("qr-reader");

    const qrCodeSuccessCallback = (decodedText, decodedResult) => {

        let newScanTime = new Date().getTime();
        if ((newScanTime - oldScanTime) > 2000) {
            pause();
            callBack(decodedText);
            oldScanTime = newScanTime;
            console.log(decodedText)
        }
    }
    const config = {
        fps: 10, qrbox: { height: 250, width: 250 }
    };

    // If you want to prefer back camera
    html5QrCode.start({ facingMode: "environment" }, config, qrCodeSuccessCallback);
}

function resume() {
    html5QrCode.resume();
}

function stop() {
    html5QrCode.stop();
}

function pause() {
    html5QrCode.pause();
}