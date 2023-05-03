import { Profile } from "./Account.js"

export class Restaurant {
  constructor (data) {
    this.name = data.name
    this.imgUrl = data.imgUrl
    this.description = data.description
    this.isShutdown = data.isShutdown
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
    this.id = data.id
    this.createdAt = data.createdAt
    this.updatedAt = data.updatedAt
  }
}
