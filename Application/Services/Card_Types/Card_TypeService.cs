namespace Application.Services.Card_Types
{
    public class Card_TypeService: ICard_TypeService
    {
        private readonly ICard_TypeRepository _cardTypeRepository;
        private readonly IMapper _mapper;

        public Card_TypeService(ICard_TypeRepository cardTypeRepository, IMapper mapper)
        {
            _cardTypeRepository = cardTypeRepository;
            _mapper = mapper;
        }

        // Create Method
        public async Task<ResultView<CreateCard_TypeDTO>> CreateAsync(CreateCard_TypeDTO entity)
        {
            ResultView<CreateCard_TypeDTO> result = new();
            try
            {
                // Check if the Card Type with the same name already exists
                bool exist = (await _cardTypeRepository.GetAllAsync()).Any(ct => ct.Name == entity.Name);
                if (exist)
                {
                    result = new ResultView<CreateCard_TypeDTO>
                    {
                         IsSuccess = false,
                        Massage = "Card Type Already Exists"
                    };
                    return result;
                }

                var cardType = new Card_Type
                {
                    Name = entity.Name
                };

                var createdCardType = await _cardTypeRepository.CreateAsync(cardType);
                await _cardTypeRepository.SaveChangesAsync();

                var returnCardType = new CreateCard_TypeDTO
                {
                    Name = createdCardType.Name??string.Empty
                };

                result = new ResultView<CreateCard_TypeDTO>
                {
                    Entity = returnCardType,
                    IsSuccess = true,
                    Massage = "Card Type Created Successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<CreateCard_TypeDTO>
                {
                     IsSuccess = false,
                    Massage = "Error Occurred: " + ex.Message
                };
                return result;
            }
        }


        // Update Method
        public async Task<ResultView<UpdateCard_TypeDTO>> UpdateAsync(UpdateCard_TypeDTO entity)
        {
            ResultView<UpdateCard_TypeDTO> result = new();
            try
            {
                var cardType = await _cardTypeRepository.GetOneAsync(entity.Id);
                if (cardType == null)
                {
                    result = new ResultView<UpdateCard_TypeDTO>
                    {
                        IsSuccess = false,
                        Massage = "Card Type not found",
                     };
                    return result;
                }

                // Update the card type entity
                _mapper.Map(entity, cardType);
                await _cardTypeRepository.UpdateAsync(cardType);
                await _cardTypeRepository.SaveChangesAsync();

                var updatedCardTypeDto = _mapper.Map<UpdateCard_TypeDTO>(cardType);
                result = new ResultView<UpdateCard_TypeDTO>
                {
                    Entity = updatedCardTypeDto,
                    IsSuccess = true,
                    Massage = "Card Type Updated Successfully"
                };
                return result;
            }
            catch (Exception ex)
            {
                result = new ResultView<UpdateCard_TypeDTO>
                {
                    IsSuccess = false,
                    Massage = "Error Occurred: " + ex.Message,
                 };
                return result;
            }
        }

        // Delete Method
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var cardType = await _cardTypeRepository.GetOneAsync(id);
                if (cardType == null) return false;

                await _cardTypeRepository.DeleteAsync(cardType);
                await _cardTypeRepository.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        // Get All Method
        public async Task<List<Card_TypeDTO>> GetAllAsync()
        {
            var cardTypes = await _cardTypeRepository.GetAllAsync();
            return _mapper.Map<List<Card_TypeDTO>>(cardTypes);
        }

        // Get By Id Method
        public async Task<Card_TypeDTO> GetByIdAsync(int id)
        {
            var cardType = await _cardTypeRepository.GetOneAsync(id);
            return _mapper.Map<Card_TypeDTO>(cardType);
        }
        public async Task<bool> NameExistsAsync(string name)
        {
            return await _cardTypeRepository.ExistsAsync(name);
        }


    }

}
