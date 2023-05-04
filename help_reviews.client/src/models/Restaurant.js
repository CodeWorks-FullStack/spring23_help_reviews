import { Profile } from "./Account.js"

export class Restaurant {
  constructor (data) {
    this.id = data.id
    this.name = data.name
    this.imgUrl = data.imgUrl
    this.description = data.description
    this.isShutdown = data.isShutdown
    this.creatorId = data.creatorId
    this.creator = new Profile(data.creator)
    this.createdAt = new Date(data.createdAt)
    this.updatedAt = new Date(data.updatedAt)
    this.reportCount = data.reportCount
  }
}
