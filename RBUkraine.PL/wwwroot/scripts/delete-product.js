const confirmDeletionProduct = async (id, message, onDelete) => {
    const confirmed = confirm(message);

    if (confirmed) {
        await deleteProduct(id, onDelete);
    }
};

const deleteProduct = async (id, onDelete) => {
    return fetch(`https://localhost:5001/products/${id}/delete`,
        {
            method: "post"
        }).then(() => onDelete());
};