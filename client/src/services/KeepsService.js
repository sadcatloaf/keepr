import { logger } from "@/utils/Logger.js"
import { api } from "./AxiosService.js"
import { Keep } from "@/models/Keep.js"
import { AppState } from "@/AppState.js"

class KeepsService {
    createKeep() {
        throw new Error('Method not implemented.')
    }

    async getKeeps() {
        const response = await api.get('api/keeps')
        logger.log('Got Keeps', response.data)
        const keeps = response.data.map(keepPOJO => new Keep(keepPOJO))
        AppState.keeps = keeps
    }
}
export const keepsService = new KeepsService()