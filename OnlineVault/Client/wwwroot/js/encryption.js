window.AESEncrypt = async function (key, iv, inputText) {
    try {
        var cryptoKey = await crypto.subtle.importKey(
            "raw" /* format */,
            BytesFromBase64(key) /* keyData */,
            "AES-CBC" /* algorithm */,
            false /* extractable */,
            ["encrypt", "decrypt"] /* keyUsages */);

        var encrypted = await window.crypto.subtle.encrypt(
            {
                name: "AES-CBC",
                iv: BytesFromBase64(iv),
            },
            cryptoKey,
            inputText.buffer,
        );
    }
    catch (e) {
        return null;
    }

    return new Uint8Array(encrypted);
}

window.AESDecrypt = async function (key, iv, cipherText) {
    try {
        var cryptoKey = await crypto.subtle.importKey(
            "raw" /* format */,
            BytesFromBase64(key) /* keyData */,
            "AES-CBC" /* algorithm */,
            false /* extractable */,
            ["encrypt", "decrypt"] /* keyUsages */);

        var decrypted = await window.crypto.subtle.decrypt(
            {
                name: "AES-CBC",
                iv: BytesFromBase64(iv),
            },
            cryptoKey,
            cipherText.buffer,
        );
    }
    catch (e) {
        return null;
    }

    return new Uint8Array(decrypted);
}

window.Base64FromBytes = function (buffer) {
    var s = '';
    var uintArray = new Uint8Array(buffer);
    uintArray.filter(function (v) { s += String.fromCharCode(v); return false; });
    return window.btoa(s);
};

window.BytesFromBase64 = function (text) {
    return new Uint8Array(
        window.atob(text)
            .split('')
            .map(function (c) {
                return c.charCodeAt(0);
            })
    );
}

window.downloadFileFromStream = async (fileName, contentStreamReference) => {
    const arrayBuffer = await contentStreamReference.arrayBuffer();
    const blob = new Blob([arrayBuffer]);
    const url = URL.createObjectURL(blob);
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
    URL.revokeObjectURL(url);
}