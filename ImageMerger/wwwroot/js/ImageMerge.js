window.imageMerge = {
    merge: function (
        imgADataUrl,
        imgBDataUrl,
        orientation = "vertical",
        spacing = 0,
        background = "#ffffff",
        transparent = false,
        alignment = "center" // new parameter
    ) {
        return new Promise((resolve, reject) => {
            const imgA = new Image();
            const imgB = new Image();

            imgA.onload = () => {
                imgB.onload = () => {
                    const canvas = document.getElementById("resultCanvas");
                    const ctx = canvas.getContext("2d");

                    let width, height;
                    if (orientation === "vertical") {
                        width = Math.max(imgA.width, imgB.width);
                        height = imgA.height + imgB.height + spacing;
                    } else {
                        width = imgA.width + imgB.width + spacing;
                        height = Math.max(imgA.height, imgB.height);
                    }

                    canvas.width = width;
                    canvas.height = height;

                    if (!transparent) {
                        ctx.fillStyle = background;
                        ctx.fillRect(0, 0, width, height);
                    }

                    if (orientation === "vertical") {
                        // compute X offsets based on alignment
                        let xA = 0, xB = 0;
                        if (alignment === "center") {
                            xA = (width - imgA.width) / 2;
                            xB = (width - imgB.width) / 2;
                        } else if (alignment === "end") {
                            xA = width - imgA.width;
                            xB = width - imgB.width;
                        }
                        ctx.drawImage(imgA, xA, 0);
                        ctx.drawImage(imgB, xB, imgA.height + spacing);
                    } else {
                        // horizontal orientation → compute Y offsets
                        let yA = 0, yB = 0;
                        if (alignment === "center") {
                            yA = (height - imgA.height) / 2;
                            yB = (height - imgB.height) / 2;
                        } else if (alignment === "end") {
                            yA = height - imgA.height;
                            yB = height - imgB.height;
                        }
                        ctx.drawImage(imgA, 0, yA);
                        ctx.drawImage(imgB, imgA.width + spacing, yB);
                    }

                    resolve(canvas.toDataURL("image/png"));
                };
                imgB.onerror = reject;
                imgB.src = imgBDataUrl;
            };
            imgA.onerror = reject;
            imgA.src = imgADataUrl;
        });
    },
    download: function (canvasId, filename = "merged.png") {
        const canvas = document.getElementById(canvasId);
        if (!canvas) return;

        const link = document.createElement("a");
        link.href = canvas.toDataURL("image/png");
        link.download = filename;
        link.click();
    },
    clear: function () { 
        const canvas = document.getElementById("resultCanvas"); 
        if (canvas) { 
            const ctx = canvas.getContext("2d"); 
            ctx.clearRect(0, 0, canvas.width, canvas.height);
            canvas.width = 300;
            canvas.height = 150;
        } 
    }
};

