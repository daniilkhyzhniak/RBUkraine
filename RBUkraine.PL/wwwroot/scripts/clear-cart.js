const clear = async (onClear) => {
    return fetch(`https://localhost:5001/cart/clear`,
        {
            method: "post"
        }).then(() => onClear());
};