
export class Keep {
    constructor(data) {
        this.id = data.id
        this.createdAt = new Date(data.createdAt)
        this.updatedAt = new Date(data.updatedAt)
        this.name = data.name
        this.description = data.description
        this.img = data.img
        this.views = data.views
        this.creatorId = data.createdId
        this.creator = data.creator
        this.kept = data.kept
        this.vaultKeepId = data.vaultKeepId
    }
}




// public class Keep
// {
//     public int Id { get; set; }
//     public DateTime CreatedAt { get; set; }
//     public DateTime UpdatedAt { get; set; }
//     public string Name { get; set; }
//     public string Description { get; set; }
//     public string Img { get; set; }
//     public int Views { get; set; }
//     public string CreatorId { get; set; }
//     public Account Creator { get; set; }
//     public int Kept { get; set; }
//     public int VaultKeepId { get; set; }
// }