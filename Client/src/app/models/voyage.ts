export interface Ship {
  id: number;
  name: string;
  maximumSpeed: number;
}

export interface Port {
  id: number;
  name: string;
  country: string;
}

export interface Voyage {
  id: number;
  voyageDate: string;
  voyageStart: string;
  voyageEnd: string;
  ship: Ship;
  departurePort: Port;
  arrivalPort: Port;
}
