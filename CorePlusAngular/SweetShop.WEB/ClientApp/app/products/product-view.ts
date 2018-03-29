export class ProductView {
	constructor(
		public id?: number,
		public name?: string,
		public company?: string,
		public companyId?: number,
		public description?: string,
		public likes?: number,
		public price?: number,
		public quantity?: number,
	    public isLikedByUser?: boolean){}
}